using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Infrastructure.Abstract;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{
    public class AccountController : Controller
    {
		IAuthProvider authProvider;
		IUserRepository userRepository;
		IUserRoleRepository userRoleRepository;

		public AccountController(IAuthProvider auth, IUserRepository userRepository, IUserRoleRepository userRoleRepository)
		{
			authProvider = auth;
			this.userRepository = userRepository;
			this.userRoleRepository = userRoleRepository;
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
					return Redirect(returnUrl ?? Url.Action("index", "products"));
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

		public ActionResult Logout()
		{
			if (HttpContext.User.Identity.IsAuthenticated)
			{
				authProvider.logout();
			}
			return RedirectToAction("index", "products");
		}

		public PartialViewResult isLoggin()
		{
			string isLoggedin = HttpContext.User.Identity.IsAuthenticated.ToString();

			return PartialView("isLoggin", (string)isLoggedin);
		}

		public ActionResult Register()
		{
			TempData.Remove("userName");
			TempData.Remove("userRole");
			TempData.Add("userName", userRepository.All.ToList().Select(c => c.Name).ToList());
			TempData.Add("userRole", userRoleRepository.All.ToList().FirstOrDefault());
			return View(new User());
		}

		[HttpPost]
		public ActionResult Register(User user)
		{
			List<string> UserNames = (List<string>)TempData["userName"];
			user.UserRoleId = ((UserRole)TempData["userRole"]).UserRoleId;
			user.LastName = "Empty";
			user.FirstName = "Empty";
			user.ImageSrc = "Empty";
			user.Phone = "99999999";
			
			//user.Name = (string)TempData["UserName"];
			ModelState["Phone"].Errors.Clear();
			ModelState["LastName"].Errors.Clear();
			ModelState["FirstName"].Errors.Clear();

			if (ModelState.IsValid)
			{
				if (UserNames.IndexOf(user.Name) == -1)
				{
					//create user
					userRepository.InsertOrUpdate(user);
					userRepository.Save();
				}
				else
				{
					return RedirectToAction("Login");
				}
			}
			else
			{
				return RedirectToAction("Login");
				//return View("Login", user);
			}
			return RedirectToAction("MyAccount", "Users");
		}
    }
}
