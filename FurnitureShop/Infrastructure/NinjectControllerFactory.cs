using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using System.Configuration;
using FurnitureShop.Repository;
using FurnitureShop.Services;
using FurnitureShop.Interface;
using FurnitureShop.Infrastructure.Abstract;
using FurnitureShop.Infrastructure.Concrete;

namespace FurnitureShop.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectControllerFactory()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)_ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // Database binding // test categories are working by injection.

            //Mock<ICategoryRepository> mockCat = new Mock<ICategoryRepository>();
            //mockCat.Setup(m => m.All).Returns(new List<Category> {
            //    new Category { CategoryId = 1, Name = "Sofa" },
            //    new Category { CategoryId = 2, Name = "Chair" },
            //    new Category { CategoryId = 3, Name = "Lamp" }
            //}.AsQueryable());

            // if you enable test injection line then disable category production line
            //ninjectKernel.Bind<ICategoryRepository>().ToConstant(mockCat.Object);

            //Mock<IOrderRepository> mockOrder = new Mock<IOrderRepository>();
            //mockOrder.Setup(m => m.All).Returns(new List<Order> {
            //    new Order { OrderId = 1, OrderDate = System.DateTime.Now, OrderDeliveryId = 1},
            //    new Order { OrderId = 2, OrderDate = System.DateTime.Now, OrderDeliveryId = 3},
            //    new Order { OrderId = 3, OrderDate = System.DateTime.Now, OrderDeliveryId = 5}
            //}.AsQueryable());

            //if you enable test injection line then disable order production line
            // test link localhost:1317/orders/index
            //ninjectKernel.Bind<IOrderRepository>().ToConstant(mockOrder.Object);


            _ninjectKernel.Bind<IProductRepository>().To<ProductRepository>();
            _ninjectKernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            _ninjectKernel.Bind<IProductSubCategoryRepository>().To<ProductSubCategoryRepository>();
            _ninjectKernel.Bind<ISubCategoryRepository>().To<SubCategoryRepository>();
            _ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
            _ninjectKernel.Bind<IOrderRepository>().To<OrderRepository>();
            _ninjectKernel.Bind<IOrderProductRepository>().To<OrderProductRepository>();
            _ninjectKernel.Bind<IOrderDeliveryRepository>().To<OrderDeliveryRepository>();
            _ninjectKernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            _ninjectKernel.Bind<IUserRoleRepository>().To<UserRoleRepository>();
            _ninjectKernel.Bind<IAddressRepository>().To<AddressRepository>();
            _ninjectKernel.Bind<IRatePlusCommentRepository>().To<RatePlusCommentRepository>();
            _ninjectKernel.Bind<ISpecialOfferRepository>().To<SpecialOfferRepository>();

            // new lines order processing
            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                .AppSettings["Email.WriteAsFile"] ?? "false")
            };
            _ninjectKernel.Bind<IOrderProcessor>()
            .To<EmailOrderProcessor>()
            .WithConstructorArgument("settings", emailSettings);
        }
    }
}
