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
    ///     Classe responsável por disponibilizar todos os métodos da API disponíveis para manipular o recurso Livro
    ///     existente no domínio da aplicação
    /// </summary>
    [RoutePrefix("Api/Livros")]
    public class LivrosController : ApiController
    {
        private readonly ILivrosApplication _livrosApplication;

        public LivrosController(ILivrosApplication livrosApplication)
        {
            _livrosApplication = livrosApplication;
        }

        /// <summary>
        /// Método responsável por obter um livro pelo seu identificador
        /// </summary>
        /// <param name="livroId">identificador do livro</param>
        /// <returns></returns>
        [Route("ObterLivro/{livroId}", Name = "GetBookById")]
        [HttpGet]
        [Cache(Client = 30, Server = 30)]
        public Task<HttpResponseMessage> ObterLivro([FromUri] Guid livroId)
        {
            HttpResponseMessage response = null;

            var livro = _livrosApplication.GetById(livroId);

            if (livro == null)
            {
                const string message = "Desculpe, mas nenhum livro foi encontrado para o identificador informado.";
                response = Request.CreateResponse(HttpStatusCode.NoContent, message);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, livro);
            }

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

        /// <summary>
        /// Método responsável por buscar livros de acordo com um determinado filtro informado
        /// </summary>
        /// <param name="filtros">Filtros informados</param>
        /// <param name="pagina">Utilizado para paginação de registros</param>
        /// <returns></returns>
        [HttpGet, Route("PesquisarLivros")]
        [Cache(Client = 30, Server = 30)]
        public Task<HttpResponseMessage> PesquisarLivros([FromUri] PesquisaDTO filtros, int? pagina)
        {
            HttpResponseMessage response = null;

            var livros = _livrosApplication.PesquisarLivros(filtros, PagingHelper.GetOffset(pagina), PagingHelper.GetTake());

            if (!livros.Dto.Any())
            {
                const string message =
                    "Desculpe, não foi possível encontrar nenhum resultado para os filtros utilizados.";
                response = Request.CreateResponse(HttpStatusCode.NoContent, message);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, livros);
            }

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

        /// <summary>
        /// Método utilizado para recuperar os livros de acordo com um titulo informado
        /// </summary>
        /// <param name="filtros">Filtros informados</param>
        /// <param name="pagina">Utilizado para paginação de registros</param>
        /// <returns></returns>
        [HttpGet, Route("LivroPorTitulo")]
        [Cache(Client = 30, Server = 30)]
        public Task<HttpResponseMessage> LivroPorTitulo([FromUri] LivrosDTO filtros, int? pagina)
        {
            HttpResponseMessage response = null;

            if (filtros.Titulo == null || filtros.UsuarioId == Guid.Empty)
            {
                const string message = "Por favor, informe o titulo do livro e o identificador do usuário!";
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }

            var livro = _livrosApplication.BuscarPorTitulo(filtros, PagingHelper.GetOffset(pagina),
                PagingHelper.GetTake());

            //var livro = _livrosApplication.BuscarPorTitulo(filtros, skip, take);

            if (!livro.Dto.Any())
            {
                const string message =
                    "Desculpe, não foi possível encontrar nenhum resultado para os filtros utilizados.";
                response = Request.CreateResponse(HttpStatusCode.NoContent, message);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, livro);
            }

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

        /// <summary>
        /// Método utilizado para buscar todos os livros com uma determinada meta registrada
        /// </summary>
        /// <param name="filtros">Filtros informados</param>
        /// <param name="pagina">Utilizado para paginação de registros</param>
        /// <returns></returns>
        [HttpGet, Route("LivrosPorMeta")]
        [Cache(Client = 30, Server = 30)]
        public Task<HttpResponseMessage> LivrosPorMeta([FromUri] PesquisaDTO filtros, int? pagina)
        {
            HttpResponseMessage response = null;

            if (filtros.MetaDominioId == Guid.Empty || filtros.UsuarioId == Guid.Empty)
            {
                const string message = "Por favor, informe o identificador da Meta e o identificador do usuário!";
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }

            var livro = _livrosApplication.LivrosPorMeta(filtros, PagingHelper.GetOffset(pagina),
                PagingHelper.GetTake());

            if (!livro.Dto.Any())
            {
                const string message =
                    "Desculpe, não foi possível encontrar nenhum resultado para os filtros utilizados.";
                response = Request.CreateResponse(HttpStatusCode.NoContent, message);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, livro);
            }

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

        /// <summary>
        /// Método responsável por retornar todos os livros registrados de uma determinada categoria
        /// </summary>
        /// <param name="filtros">Filtros informados</param>
        /// <param name="pagina">Utilizado para paginação de registros</param>
        /// <returns></returns>
        [HttpGet, Route("LivrosPorCategoria")]
        [Cache(Client = 30, Server = 30)]
        public Task<HttpResponseMessage> LivrosPorCategoria([FromUri] LivrosDTO filtros, int? pagina)
        {
            HttpResponseMessage response = null;

            if (filtros.CategoriaId == Guid.Empty || filtros.UsuarioId == Guid.Empty)
            {
                const string message = "Por favor, informe o identificador da categoria e o identificador do usuário!";
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, message));
            }

            var livro = _livrosApplication.BuscarPorTitulo(filtros, PagingHelper.GetOffset(pagina),
                PagingHelper.GetTake());

            if (!livro.Dto.Any())
            {
                const string message =
                    "Desculpe, não foi possível encontrar nenhum resultado para os filtros utilizados.";
                response = Request.CreateResponse(HttpStatusCode.NoContent, message);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, livro);
            }

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

        /// <summary>
        /// Método responsável por obter todos os livros existentes para um determinado usuário
        /// </summary>
        /// <param name="filtros">Somente será utilizado o identificador do usuário dentro do DTO</param>
        /// <param name="pagina">Utilizado para paginação de registros</param>
        /// <returns></returns>
        [HttpGet, Route("ObterTodosLivros")]
        [Cache(Client = 30, Server = 30)]
        public Task<HttpResponseMessage> ObterTodosLivros([FromUri] LivrosDTO filtros, int? pagina)
        {
            HttpResponseMessage response = null;

            var livros = _livrosApplication.GetAll(filtros, PagingHelper.GetOffset(pagina),
                PagingHelper.GetTake());

            if (!livros.Dto.Any())
                response = Request.CreateResponse(HttpStatusCode.NoContent, new { message = "Desculpe, não foi possível encontrar nenhum resultado para os filtros utilizados." });
            else
                response = Request.CreateResponse(HttpStatusCode.OK, livros);

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

        /// <summary>
        /// Método responsável por cadastrar uma nova leitura de um livro no domínio da aplicação
        /// </summary>
        /// <param name="livros">Data Transfer Object do livro a ser cadastrado</param>
        /// <returns></returns>
        [HttpPost, Route("CadastrarLivro")]
        public Task<HttpResponseMessage> CadastrarLivro(LivrosDTO livros)
        {
            HttpResponseMessage response = null;

            if (!ModelState.IsValid || livros == null)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));


            var livroAdicionado = _livrosApplication.Add(livros);

            if (!livroAdicionado.ValidationResult.IsValid)
            {
                foreach (var item in livroAdicionado.ValidationResult.Erros)
                {
                    ModelState.AddModelError(string.Empty, item.Message);
                }

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState));
            }

            response = Request.CreateResponse(HttpStatusCode.Created, new { message = "Leitura do livro foi criada com sucesso!" });
            //var newUriRecurso = Url.Link("GetBookById", new { id = livroAdicionado.LivroId, controller = "livros" });
            //response.Headers.Location = new Uri(newUriRecurso);

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }

        /// <summary>
        /// Método responsável por deletar uma leitura de uma livro da base da aplicação
        /// </summary>
        /// <param name="livroId">Identificador do livro</param>
        /// <returns></returns>
        [HttpDelete, Route("DeletarLivro/{livroId}")]
        public Task<HttpResponseMessage> DeletarLivro([FromUri] Guid livroId)
        {
            HttpResponseMessage response = null;

            _livrosApplication.Delete(livroId);

            response = Request.CreateResponse(HttpStatusCode.OK, new { message = "Leitura do livro foi deletada com sucesso!" });

            var tarefa = new TaskCompletionSource<HttpResponseMessage>();
            tarefa.SetResult(response);
            return tarefa.Task;
        }
    }
}
