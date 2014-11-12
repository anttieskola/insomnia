using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace insomnia.Api
{
    // took this from http://www.asp.net/web-api/overview/advanced/dependency-injection
    // because did not want to install mvc package as it depends on mvc & razor
    public sealed class UnityResolver : IDependencyResolver
    {
        private IUnityContainer container;
        private bool disposed;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        ~UnityResolver()
        {
            this.Dispose(false);
        }

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

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            lock (this)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        // managed
                        container.Dispose();
                    }
                    // unmanaged
                    this.disposed = true;
                }
            }
        }
    }
}
