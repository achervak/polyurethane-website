using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Ninject;
using Polyurethane.Data.Implementation;
using Polyurethane.Data.Implementation.Communication;
using Polyurethane.Data.Implementation.DataManagers;
using Polyurethane.Data.Interfaces;
using Polyurethane.Data.Interfaces.Communication;
using Polyurethane.Data.Interfaces.DataManagers;
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
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PolyurethaneDb"].ConnectionString;
                return new EfDataProvider(connectionString);
            }).InSingletonScope();

            _kernel.Bind<IEmailCommunication>().To<EmailCommunication>();
            _kernel.Bind<IOrderDataManager>().To<OrderDataManager>();
            _kernel.Bind<IMapper>().ToMethod(_ => AutoMapperResolver.InitMapper()).InSingletonScope();
            _kernel.Bind<IEventLogger>().To<EventLogger>();
            _kernel.Bind<IConfigurationManager>().To<Polyurethane.Data.Implementation.DataManagers.ConfigurationManager>();
            _kernel.Bind<ITemplateManager>().To<TemplateManager>();
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