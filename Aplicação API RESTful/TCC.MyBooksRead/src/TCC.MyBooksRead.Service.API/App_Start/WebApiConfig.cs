using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TCC.MyBooksRead.Application;
using TCC.MyBooksRead.Application.Interfaces;
using TCC.MyBooksRead.Domain.Interfaces.CrossCutting;
using TCC.MyBooksRead.Domain.Interfaces.Repository;
using TCC.MyBooksRead.Domain.Interfaces.Services;
using TCC.MyBooksRead.Domain.Services;
using TCC.MyBooksRead.Infra.CrossCutting.Filters;
using TCC.MyBooksRead.Infra.CrossCutting.Helpers;
using TCC.MyBooksRead.Infra.Data.Repositories;
using WebApiContrib.IoC.Unity;

namespace TCC.MyBooksRead.Service.API
{
    public static class WebApiConfig
    {
        /// <summary>
        /// Classe para configurações gerais da API
        /// </summary>
        /// <param name="config">Instância da Classe HttpConfiguration</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            #region Registro de rotas para acessar API

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            #endregion

            #region Configuração IoC Usando Unity


            var container = new UnityContainer();

            //Camada de Application
            container.RegisterType<ILivrosApplication, LivrosApplication>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoriasApplication, CategoriasApplication>(new HierarchicalLifetimeManager());
            container.RegisterType<IMetasApplication, MetasApplication>(new HierarchicalLifetimeManager());

            //Camada de Service Domain
            container.RegisterType<ILivrosService, LivrosService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoriasService, CategoriasService>(new HierarchicalLifetimeManager());
            container.RegisterType<IMetasService, MetasService>(new HierarchicalLifetimeManager());
            container.RegisterType<IEnumExtender, EnumExtensionMethods>(new HierarchicalLifetimeManager());
            
            //Camada de Repository
            container.RegisterType<ILivrosRepository, LivrosRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAutoresRepository, AutoresRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoriasRepository, CategoriasRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMetasPorLivrosRepository, MetasPorLivrosRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMetasRepository, MetasRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUsuariosRepository, UsuariosRepository>(new HierarchicalLifetimeManager());
            
            config.DependencyResolver = new UnityResolver(container);

            #endregion

            #region Configuração Media Types 

            //Removendo o Media Type XML a fim de deixar a aplicação mais performática

            var formatos = GlobalConfiguration.Configuration.Formatters;
            formatos.Remove(formatos.XmlFormatter);

            //Configurando o retorno de dados JSON - JavaScript Object Notation

            var mediaTypeJSON = formatos.JsonFormatter.SerializerSettings;
            mediaTypeJSON.Formatting = Formatting.Indented;
            mediaTypeJSON.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Ao encontrar uma referência cíclica a mesma é ignorada para não lançar uma exceção
            mediaTypeJSON.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            #endregion

            #region Registrando filtros de error

            config.Filters.Add(new GlobalActionHandler());
            config.Services.Replace(typeof(IExceptionHandler), new GeneralExceptionApplication());

            #endregion

            #region Habilitando CORS

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            #endregion
        }
    }
}
