using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace TCC.MyBooksRead.Infra.CrossCutting.Filters
{
    public class GlobalActionHandler : ActionFilterAttribute
    {

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var http = actionExecutedContext.Exception as HttpResponseException;
            if (http != null)
            {
                var errorHttp = http;
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(errorHttp.Response.StatusCode,
                    http.Message);
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
