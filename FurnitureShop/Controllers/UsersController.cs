using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class UsersController : Controller
    {
		private readonly IUserRoleRepository userroleRepository;
		private readonly IUserRepository userRepository;
		private readonly IAddressRepository addressRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        /*public UsersController() : this(new UserRoleRepository(), new UserRepository())
        {
        }*/

		public UsersController(IUserRoleRepository userroleRepository, IUserRepository userRepository, IAddressRepository addressRepository)
        {
			this.userroleRepository = userroleRepository;
			this.userRepository = userRepository;
			this.addressRepository = addressRepository;
        }

        //
        // GET: /Users/
		[Authorize(Roles = "Admin, Editor")]
        public ViewResult Index()
        {
            return View(userRepository.AllIncluding(user => user.Address));
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(int id)
        {
            return View(userRepository.Find(id));
        }

        //
        // GET: /Users/Create
		[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
			ViewBag.PossibleUserRoles = userroleRepository.All;

			User myNewUser = new User()
			{
				Address = new List<Address>() { new Address()}
			};

			return View(myNewUser);
        } 

        //
        // POST: /Users/Create

        [HttpPost]
		[Authorize(Roles = "Admin")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUserRoles = userroleRepository.All;
				return View(user);
			}
        }
        
        //
        // GET: /Users/Edit/5
		[HttpGet]
		[Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleUserRoles = userroleRepository.All;
             return View(userRepository.Find(id));
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
		[Authorize(Roles = "Admin")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUserRoles = userroleRepository.All;
				return View(user);
			}
        }

		[Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(userRepository.Find(id));
        }
		
        [HttpPost, ActionName("Delete")]
		[Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            userRepository.Delete(id);
            userRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                userroleRepository.Dispose();
                userRepository.Dispose();
            }
            base.Dispose(disposing);
        }

		//
		// Show account for the current user
		//
		[Authorize]
		public ViewResult myAccount()
		{
			//Get the current user 
			string userName = HttpContext.User.Identity.Name;
			User user = userRepository.AllIncluding(u => u.Address).FirstOrDefault(u => u.Name == userName);
			return View(user);
		}
		[Authorize]
		public ActionResult EditUserInfo()
		{
			//Get the current user 
			string userName = HttpContext.User.Identity.Name;
			User user = userRepository.All.FirstOrDefault(u => u.Name == userName);

			//Is user authenticated? returns true or false
			bool userAuth = HttpContext.User.Identity.IsAuthenticated;
			TempData.Remove("UserId");
			TempData.Remove("UserName");
			TempData.Remove("UserUserRoleId");
			TempData.Add("UserId", user.UserId);
			TempData.Add("UserName", user.Name);
			TempData.Add("UserUserRoleId", user.UserRoleId);

			return View(userRepository.Find(user.UserId));
		}

		[HttpPost]
		[Authorize]
		public ActionResult EditUserInfo(User user)
		{
			//Get the authorized user
			string userName = HttpContext.User.Identity.Name;
			//User authedUser = userRepository.All.FirstOrDefault(u => u.Name == userName);
			user.UserRoleId = (int)TempData["UserUserRoleId"];
			user.UserId = (int)TempData["UserId"];
			user.Name = (string)TempData["UserName"];
			ModelState["Name"].Errors.Clear();
			if (ModelState.IsValid)
			{
				userRepository.InsertOrUpdate(user);
				userRepository.Save();
				return RedirectToAction("MyAccount");
			}
			else
			{
				ViewBag.PossibleUserRoles = userroleRepository.All;
				return View(user);
			}
		}
		[Authorize]
		public ActionResult EditAddress(int AddressId)
		{
			Address address = addressRepository.Find(AddressId);

			//Check with the user Id
			string userName = HttpContext.User.Identity.Name;
			User user = userRepository.All.FirstOrDefault(u => u.Name == userName);

			if (address.UserId != user.UserId) {
				return RedirectToAction("MyAccount");
			}

			return View(addressRepository.Find(AddressId));
		}
		[HttpPost]
		[Authorize]
		public ActionResult EditAddress(Address address)
		{
			if (ModelState.IsValid)
			{
				addressRepository.InsertOrUpdate(address);
				addressRepository.Save();
				return RedirectToAction("MyAccount");
			}
			else
			{
				return View(address);
			}
		}
		[Authorize]
		public ActionResult AddAddress()
		{
			return View();
		}

		[HttpPost]
		[Authorize]
		public ActionResult AddAddress(Address address)
		{
			//Check with the user Id
			string userName = HttpContext.User.Identity.Name;
			User user = userRepository.All.FirstOrDefault(u => u.Name == userName);

			address.UserId = user.UserId;
			ModelState["UserId"].Errors.Clear();

			if (ModelState.IsValid)
			{
				addressRepository.InsertOrUpdate(address);
				addressRepository.Save();
				return RedirectToAction("MyAccount");
			}
			else
			{
				return View(address);
			}
		}
		[Authorize]
		public ActionResult RemoveAddress(int AddressId)
		{
			return View(addressRepository.Find(AddressId));
		}

		[HttpPost]
		[Authorize]
		public ActionResult RemoveAddressConfirmed(int AddressId)
		{
			Address address = addressRepository.Find(AddressId);

			//Check with the user Id
			string userName = HttpContext.User.Identity.Name;
			User user = userRepository.All.FirstOrDefault(u => u.Name == userName);

			if (address.UserId == user.UserId)
			{
				addressRepository.Delete(AddressId);
				addressRepository.Save();
				return RedirectToAction("MyAccount");
			}
			else
			{
				return RedirectToAction("MyAccount");
			}
		}
    }
}

