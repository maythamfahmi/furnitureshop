using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Infrastructure.Abstract;

namespace FurnitureShop.Controllers
{
    public class AccountController : Controller
    {
		IAuthProvider authProvider;

		public AccountController(IAuthProvider auth)
		{
			authProvider = auth;
		}

		public ViewResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginOnViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (authProvider.Authenticate(model.userName, model.Password))
				{
					return Redirect(returnUrl ?? Url.Action("index"));
				}
				else
				{
					ModelState.AddModelError("", "Incorrect username or password");
					return View();
				}
			}
			else
			{
				return View();
			}
		}

    }
}
