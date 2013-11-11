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
using FurnitureShop.Services;
using FurnitureShop.Interface;

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

            //Mock<ICategoryRepository> mockCat = new Mock<ICategoryRepository>();
            //mockCat.Setup(m => m.All).Returns(new List<Category> {
            //    new Category { CategoryId = 1, Name = "Sofa" },
            //    new Category { CategoryId = 2, Name = "Chair" },
            //    new Category { CategoryId = 3, Name = "Lamp" }
            //}.AsQueryable());
            
            //Mock<IProductRepository> mockPro = new Mock<IProductRepository>();
            //mockPro.Setup(m => m.All).Returns(new List<Product> {
            //    new Product { Name = "1", Price = 125, Description = "test", ImageSrc="", Category = new Category{ Name = "sofa" } },
            //    new Product { Name = "2", Price = 225, Description = "test", ImageSrc="", Category = new Category{ Name = "sofa" } },
            //    new Product { Name = "3", Price = 325, Description = "test", ImageSrc="", Category = new Category{ Name = "sofa" } }
            //}.AsQueryable());

            //ninjectKernel.Bind<ICategoryRepository>().ToConstant(mockCat.Object);
            //ninjectKernel.Bind<IProductRepository>().ToConstant(mockPro.Object);

            ninjectKernel.Bind<IProductRepository>().To<ProductRepository>();
            ninjectKernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
            ninjectKernel.Bind<IOrderRepository>().To<OrderRepository>();
            ninjectKernel.Bind<IOrderProductRepository>().To<OrderProductRepository>();
            ninjectKernel.Bind<IOrderDeliveryRepository>().To<OrderDeliveryRepository>();
            
            // new lines
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                .AppSettings["Email.WriteAsFile"] ?? "false")
            };
            ninjectKernel.Bind<IOrderProcessor>()
            .To<EmailOrderProcessor>()
            .WithConstructorArgument("settings", emailSettings);
        }
    }
}