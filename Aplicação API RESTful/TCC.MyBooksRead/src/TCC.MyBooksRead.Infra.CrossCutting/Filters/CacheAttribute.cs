using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TCC.MyBooksRead.Infra.CrossCutting.Cache;

namespace TCC.MyBooksRead.Infra.CrossCutting.Filters
{
    /// <summary>
    /// Classe responsável por configurar o cache da API
    /// </summary>
    public class CacheAttribute : ActionFilterAttribute
    {
        private string _cachekey;
        private static readonly ObjectCache Cache = MemoryCache.Default;

        public int Server { get; set; }
        public int Client { get; set; }

        /// <summary>
        /// Método Responsável por verificar se o contexto corrente HTTP em execução permite cache, o que segundo a RFC 2616
        /// somente métodos GET e HEAD podem utilizar cache
        /// </summary>
        /// <param name="property">Server ou Client</param>
        /// <param name="ac">Informações corrente da action em execução</param>
        /// <returns></returns>
        private bool IsCacheable(int property, HttpActionContext ac)
        {
            if (property <= 0)
                return false;

            return ac.Request.Method == HttpMethod.Get || ac.Request.Method == HttpMethod.Head;
        }

        /// <summary>
        /// Configurações do cache no lado do cliente, possibilitando a duração em segundos e a revalidação do mesmo
        /// sempre que uma solicitação for depreciada.
        /// </summary>
        /// <returns></returns>
        private CacheControlHeaderValue GetClientCache()
        {
            var cacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromSeconds(Client),
                MustRevalidate = true
            };

            return cacheControl;
        }

        /// <summary>
        /// Antes de executar uma determinada action é verificado se o que o usuário está solicitando já se encontra
        /// armazenado em cache. Para tal busca é utilizado a chave "_cachekey", caso já exista é devolvido o conteúdo
        /// para o cliente no formato padrão "application/json".
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!IsCacheable(Server, actionContext)) return;

            var accept = actionContext.Request.Headers.Accept.FirstOrDefault() ??
                         new MediaTypeHeaderValue("application/json");
            _cachekey = string.Format("{0}|{1}", actionContext.Request.RequestUri.PathAndQuery, accept);

            var cachedResponseContent = Cache.Get(_cachekey) as WebApiCacheItem;
            if (cachedResponseContent == null || !cachedResponseContent.IsValid()) return;

            actionContext.Response = actionContext.Request.CreateResponse();
            actionContext.Response.Content = new ByteArrayContent(cachedResponseContent.Content);
            actionContext.Response.Content.Headers.ContentType = new MediaTypeHeaderValue(cachedResponseContent.ContentType);

            if (IsCacheable(Client, actionContext))
                actionContext.Response.Headers.CacheControl = GetClientCache();
        }
        
        /// <summary>
        /// Após a action ser executada, é verificado se no Cache da API ja contém a _cacheKey corrente, caso nao tenha
        /// é adicionado a mesma com seu respectivo tempo de duração.
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (!Cache.Contains(_cachekey))
            {
                var body = await actionExecutedContext.Response.Content.ReadAsByteArrayAsync();
                var cacheItem = new WebApiCacheItem(actionExecutedContext.Response.Content.Headers.ContentType, body);
                Cache.Add(_cachekey, cacheItem, DateTime.Now.AddSeconds(Server));
            }

            if (IsCacheable(Client, actionExecutedContext.ActionContext))
            {
                actionExecutedContext.ActionContext.Response.Headers.CacheControl = GetClientCache();
            }
        }
    }
}
