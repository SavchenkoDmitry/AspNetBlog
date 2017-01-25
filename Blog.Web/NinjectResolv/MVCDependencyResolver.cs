using Ninject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.Services.Interfaces;
using Blog.Services.Services;
using Blog.DAL.Repositories;

namespace Blog.Web.NinjectResolv
{
    public class MVCDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public MVCDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IAccountService>().To<AccountService>();
        }

    }
}