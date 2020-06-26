using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TCC.MyBooksRead.Application.Interfaces;
using TCC.MyBooksRead.Infra.CrossCutting.Filters;

namespace TCC.MyBooksRead.Service.API.Controllers
{
    [RoutePrefix("Api/Categorias")]
    public class CategoriasController : ApiController
    {
        private readonly ICategoriasApplication _categoriasApplication;

        public CategoriasController(ICategoriasApplication categoriasApplication)
        {
            _categoriasApplication = categoriasApplication;
        }

        [HttpGet, Route("ObterCategorias")]
        [Cache(Client = 30, Server = 30)]
        public Task<HttpResponseMessage> ObterCategorias()
        {
            HttpResponseMessage response = null;

            var categorias =_categoriasApplication.BuscarCategoriasDominio();

            response = Request.CreateResponse(HttpStatusCode.OK, categorias);

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }
    }
}
