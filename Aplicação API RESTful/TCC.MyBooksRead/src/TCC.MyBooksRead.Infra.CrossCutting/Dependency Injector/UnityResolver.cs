using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace TCC.MyBooksRead.Infra.CrossCutting.Dependency_Injector
{
    /// <summary>
    /// Interface IDependencyResolver utilizada para pegar as instâncias das classe
    /// </summary>
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        /// <summary>
        /// Chamado após criar o controller para injetar a dependência
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns>Instância de um objeto</returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        /// Chamado após criar o controller para injetar a dependência
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns>Lista com instâncias de vários objetos</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        /// <summary>
        /// Chamado no momento em que o Web Api irá criar o Controller
        /// </summary>
        /// <returns>Representa o filho do Scope</returns>
        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}