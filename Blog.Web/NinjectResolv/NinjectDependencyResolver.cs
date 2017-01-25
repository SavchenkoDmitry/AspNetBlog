using Ninject;
using Ninject.Syntax;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Services.Interfaces;
using Blog.Services.Services;
using Blog.DAL.Repositories;
using System.Web.Http.Dependencies;

namespace Blog.Web.NinjectResolv
{
    public class NinjectDependencyScope : IDependencyScope
    {
        IResolutionRoot resolver;

        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolver.TryGet(serviceType);
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return resolver.GetAll(serviceType);
        }

        public void Dispose()
        {
            IDisposable disposable = resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            resolver = null;
        }
    }

    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }
        private void AddBindings()
        {
            kernel.Bind<IBlogService>().To<BlogService>();
            //kernel.Bind<IAccountService>().To<AccountService>();
        }
    }
}