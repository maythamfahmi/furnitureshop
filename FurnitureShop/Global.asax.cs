using FurnitureShop.Infrastructure;
using FurnitureShop.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

	internal class myNinjectModule : NinjectModule
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
		private IKernel ninjectKernel = new StandardKernel(new myNinjectModule());
        protected void Application_Start()
        {
            // dont use this System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<FurnitureShop.Models.FurnitureShopContext, Configuration>());
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<FurnitureShop.Models.FurnitureShopContext>());
            
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            // new line
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
			// inject user repository into custom membership provider and custom role provider
			ninjectKernel.Inject(Membership.Provider);
			ninjectKernel.Inject(Roles.Provider);
        }
    }
}