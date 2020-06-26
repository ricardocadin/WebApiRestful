using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TCC.MyBooksRead.Application.DTO;
using TCC.MyBooksRead.Application.Interfaces;
using TCC.MyBooksRead.Infra.CrossCutting.Filters;
using TCC.MyBooksRead.Infra.CrossCutting.Helpers;

namespace TCC.MyBooksRead.Service.API.Controllers
{
    /// <summary>
    ///     Classe responsável por disponibilizar todos os métodos da API disponíveis para manipular o recurso Meta
    ///     existente no domínio da aplicação
    /// </summary>
    [RoutePrefix("Api/Metas")]
    public class MetasController : ApiController
    {
        private readonly IMetasApplication _metasApplication;

        public MetasController(IMetasApplication metasApplication)
        {
            _metasApplication = metasApplication;
        }

        /// <summary>
        /// Método responsável por obter todas as metas passíveis de cadastro existentes no domínio da aplicação
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("ObterMetas")]
        [Cache(Client = 30, Server = 30)]
        public Task<HttpResponseMessage> ObterMetas()
        {
            HttpResponseMessage response = null;

            var metas = _metasApplication.BuscarMetasDominio();

            response = Request.CreateResponse(HttpStatusCode.OK, metas);

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }
        
        /// <summary>
        /// Método responsável por obter todas as metas cadastradas para um determinado livro
        /// </summary>
        /// <param name="livroId">Identificador do livro</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns></returns>
        [HttpGet, Route("Livro/{livroId}/MetasPorLivro")]
        [Cache(Client = 30, Server = 30)]
        public Task<HttpResponseMessage> MetasPorLivro(Guid livroId, [FromUri] int? pagina)
        {
            HttpResponseMessage response = null;

            var metasLivros = _metasApplication.MetasPorLivro(livroId, PagingHelper.GetOffset(pagina), PagingHelper.GetTake());

            if (!metasLivros.Dto.Any())
            {
                const string message = "Desculpe, não existem metas para o livro informado.";
                response = Request.CreateResponse(HttpStatusCode.NoContent, message);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, metasLivros);
            }

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

        /// <summary>
        /// Método responsável por criar uma nova meta no sistema
        /// </summary>
        /// <param name="meta">Data Transfer Object de Meta</param>
        /// <returns></returns>
        [HttpPost, Route("CriarMeta")]
        public Task<HttpResponseMessage> CriarMeta([FromBody] MetasLivrosDTO meta)
        {
            HttpResponseMessage response = null;

            if (!ModelState.IsValid || meta == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            var metaAdicionada = _metasApplication.Add(meta);

            if (!metaAdicionada.ValidationResult.IsValid)
            {
                foreach (var item in metaAdicionada.ValidationResult.Erros)
                {
                    ModelState.AddModelError(string.Empty, item.Message);
                }

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            response = Request.CreateResponse(HttpStatusCode.Created, new { message = "Meta foi adicionada com sucesso!" });

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

        /// <summary>
        /// Método responsável por obter determinada meta por identificador
        /// </summary>
        /// <param name="metaId">Identificador da meta</param>
        /// <returns></returns>
        [HttpGet, Route("ObterMeta/{metaId}")]
        [Cache(Client = 30, Server = 30)]
        public Task<HttpResponseMessage> ObterMeta([FromUri] Guid metaId)
        {
            HttpResponseMessage response = null;

            var meta = _metasApplication.GetById(metaId);

            if (meta == null)
            {
                const string message = "Desculpe, mas nenhuma meta foi encontrada para o identificador informado.";
                response = Request.CreateResponse(HttpStatusCode.NoContent, message);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, meta);
            }
            
            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

        /// <summary>
        /// Método responsável por atualizar determinanda meta existente no domínio da aplicação
        /// </summary>
        /// <param name="meta">Data Transfer Object de Meta</param>
        /// <returns></returns>
        [HttpPut, Route("AtualizarMeta")]
        public Task<HttpResponseMessage> AtualizarMeta([FromBody] MetasLivrosDTO meta)
        {
            HttpResponseMessage response = null;

            if (!ModelState.IsValid || meta == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));

            var metaAtualizada = _metasApplication.Update(meta);

            if (!metaAtualizada.ValidationResult.IsValid)
            {
                foreach (var item in metaAtualizada.ValidationResult.Erros)
                {
                    ModelState.AddModelError(string.Empty, item.Message);
                }

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            const string message = "Meta atualizada com sucesso!";
            response = Request.CreateResponse(HttpStatusCode.OK, new {message});

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

        /// <summary>
        /// Método responsável por deletar uma meta do domínio da aplicação
        /// </summary>
        /// <param name="metaId"></param>
        /// <returns></returns>
        [HttpDelete, Route("DeletarMeta/{metaId}")]
        public Task<HttpResponseMessage> DeletarMeta([FromUri] Guid metaId)
        {
            HttpResponseMessage response = null;

            _metasApplication.Delete(metaId);

            response = Request.CreateResponse(HttpStatusCode.OK, new {message = "Meta foi deletada com sucesso!"});

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

    }
}
