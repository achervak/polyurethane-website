using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Polyurethane.Data.Interfaces;
using Polyurethane.Data.Providers;

namespace polyurethane_website.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IDataProvider>().ToMethod( context =>
            {
                var connectionString = ConfigurationManager.ConnectionStrings["PolyurethaneDb"].ConnectionString;
                return new EfDataProvider(connectionString);
            }).InSingletonScope();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}