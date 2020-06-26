using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace TCC.MyBooksRead.Infra.CrossCutting.Filters
{
    /// <summary>
    ///     ExceptionHandler utilizado para capturar as exceptions não tratadas dentro da aplicação
    /// </summary>
    public class GeneralExceptionApplication : ExceptionHandler
    {
        private const string message =
            "Serviço Indisponível, tente novamente mais tarde ou entre em contato com o administrador!";

        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new ResponseMessageResult(
                context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError,message));
        }
    }
}
