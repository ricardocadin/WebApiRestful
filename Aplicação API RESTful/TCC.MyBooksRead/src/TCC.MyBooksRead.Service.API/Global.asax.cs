using System.Web;
using System.Web.Http;
using TCC.MyBooksRead.Application.AutoMapper;

namespace TCC.MyBooksRead.Service.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfiguration.RegisterMappings();
        }
    }
}