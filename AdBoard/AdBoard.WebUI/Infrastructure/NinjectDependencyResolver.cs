using AdBoard.Domain.Abstract;
using AdBoard.Domain.Entities;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdBoard.WebUI.Infrastructure
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

        public void AddBindings()
        {
            Mock<IAdRepository> moq = new Mock<IAdRepository>();
            moq.Setup(m => m.Ads).Returns(new List<Ad>
                {
                    new Ad { Title = "Motocycle", Price = 1000, Category = "Auto-Moto" },
                    new Ad { Title = "Telephone", Price = 100, Category = "Phone" },
                    new Ad { Title = "Toyota", Price = 1234, Category = "Auto-Moto"}
                });
            kernel.Bind<IAdRepository>().ToConstant(moq.Object);
        }
    }
}