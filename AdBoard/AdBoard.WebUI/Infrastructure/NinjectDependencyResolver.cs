using AdBoard.Domain.Abstract;
using AdBoard.Domain.Concrete;
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
            /*Mock<IAdRepository> moq = new Mock<IAdRepository>();
            moq.Setup(m => m.Ads).Returns(new List<Ad>
                {
                    new Ad { Name = "Motocycle", Date = new DateTime(2008,10,6), Description = "sdsadsadasd", Price = 1000, Category = "Auto-Moto", UserId = "35a86b68-803a-40fb-8522-027dedc154aa"},
                    new Ad { Name = "Telephone", Date = new DateTime(2000,10,4), Description = "asdsiadsajdkasjdkasjdkajsdklasjdlkasd",
                        Price = 100, Category = "Phone", UserId = "35a86b68-803a-40fb-8522-027dedc154aa" },
                    new Ad { Name = "Toyota", Price = 1234, Category = "Auto-Moto"}
                });
            kernel.Bind<IAdRepository>().ToConstant(moq.Object);*/
            kernel.Bind<IAdRepository>().To<EFAdRepository>();
        }
    }
}