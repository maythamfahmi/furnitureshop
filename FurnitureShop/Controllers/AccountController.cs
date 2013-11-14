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
			//Look for a user type of customer/Customer, user/User, and get first type
			UserRole UserRole = userRoleRepository.All.ToList().FirstOrDefault(u => u.Name == "user" || u.Name == "User" || u.Name == "customer" || u.Name == "Customer");
			if (UserRole == null)
			{
				UserRole = new UserRole() { Name = "user" };
				userRoleRepository.InsertOrUpdate(UserRole);
				userRoleRepository.Save();
			}
			TempData.Remove("userName");
			TempData.Remove("userRole");
			TempData.Add("userName", userRepository.All.ToList().Select(c => c.Name).ToList());
			TempData.Add("userRole", UserRole);
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginOnViewModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				if (authProvider.Authenticate(model.userName, model.Password))
				{
					return Redirect(returnUrl ?? Url.Action("MyAccount", "Users"));
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
			return RedirectToAction("index","home");
		}

		public PartialViewResult isLoggin()
		{
			string isLoggedin = HttpContext.User.Identity.IsAuthenticated.ToString();
			if (HttpContext.User.Identity.IsAuthenticated)
			{
				string userName = HttpContext.User.Identity.Name;
				User user = userRepository.All.FirstOrDefault(u => u.Name == userName);

				ViewBag.userName = user.FirstName;
			}
			
			return PartialView("isLoggin", (string)isLoggedin);
		}

		public ActionResult Register()
		{
			//Look for a user type of customer/Customer, user/User, and get first type
			UserRole UserRole = userRoleRepository.All.ToList().FirstOrDefault(u => u.Name == "user" || u.Name == "User" || u.Name == "customer" || u.Name == "Customer");
			if(UserRole == null)
			{
				UserRole = new UserRole(){ Name="user"};
				userRoleRepository.InsertOrUpdate(UserRole);
				userRoleRepository.Save();
			}
			TempData.Remove("userName");
			TempData.Remove("userRole");
			TempData.Add("userName", userRepository.All.ToList().Select(c => c.Name).ToList());
			TempData.Add("userRole", UserRole/*userRoleRepository.All.ToList().FirstOrDefault()*/);
			return View(new User());
		}

		[HttpPost]
		public ActionResult Register(User user, string returnUrl)
		{
			List<string> UserNames = (List<string>)TempData["userName"];
			UserRole userRole = (UserRole)TempData["userRole"];
			user.UserRoleId = userRole.UserRoleId;
			user.LastName = "Empty";
			user.FirstName = "Empty";
			user.ImageSrc = "Empty";
			user.Phone = "99999999";

			user.Address = new List<Address>() { new Address(){ AddressLine1="Roadname 1", City="Greatest town", Country="CountryLand", Postal="9999", UserId=user.UserId} };
			
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
					TempData["userRole"] = userRole;
					TempData["userName"] = UserNames;

					ModelState.AddModelError("Name", "Sorry, some one else is using that username");

					return View("Register", user);
				}
			}
			else
			{
				TempData["userRole"] = userRole;
				TempData["userName"] = UserNames;

				//ModelState.AddModelError("", "You'll need to add all information here");

				return View("Register", user);
				//return View("Register", "Account");
			}
			//LoginOnViewModel model = new LoginOnViewModel() { Password = user.Password, userName = user.Name };
			if (authProvider.Authenticate(user.Name, user.Password))
			{
				//return Redirect(returnUrl ?? Url.Action("index", "products"));
				return RedirectToAction("MyAccount", "Users");
			}
			else
			{
				ModelState.AddModelError("", "Incorrect username or password");
				return View();
			}
			
		}
    }
}
