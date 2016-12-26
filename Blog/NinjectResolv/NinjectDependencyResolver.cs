using Ninject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Blog.BLL.Interfaces;
using Blog.BLL.Services;
using Blog.DAL.Interfaces;
using Blog.DAL.Repositories;

namespace Blog.NinjectResolv
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
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
            kernel.Bind<IBlogService>().To<BlogService>();
            kernel.Bind<IBlogUnitOfWork>().To<BlogUnitOfWork>();
        }

    }
}