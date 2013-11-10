using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Moq;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // Database binding

            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.All).Returns(new List<Product> {
            //    new Product { Name = "1", Price = 125, Description = "test" },
            //    new Product { Name = "2", Price = 225 },
            //    new Product { Name = "3", Price = 325 }
            //}.AsQueryable());

            //ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);

            ninjectKernel.Bind<IProductRepository>().To<ProductRepository>();
            ninjectKernel.Bind<ICategoryRepository>().To<CategoryRepository>();
        }
    }
}