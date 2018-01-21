using FurnitureShop.Infrastructure;
using FurnitureShop.Binders;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FurnitureShop.Models;
using Ninject.Modules;
using FurnitureShop.Repository;
using Ninject;
using System.Web.Security;

namespace FurnitureShop
{
	internal class MyNinjectModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IUserRepository>().To<UserRepository>();
			//Bind<IAccountRoleRepository>().To<EFAccountRoleRepository>();
		}
	}
    public class MvcApplication : System.Web.HttpApplication
    {
		// Enable DI of membership provider
		private readonly IKernel _ninjectKernel = new StandardKernel(new MyNinjectModule());
        protected void Application_Start()
        {
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<FurnitureShop.Models.FurnitureShopContext>());
            
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());

			_ninjectKernel.Inject(Membership.Provider);
			_ninjectKernel.Inject(Roles.Provider);
        }
    }
}