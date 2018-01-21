using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class UsersController : Controller
    {
		private readonly IUserRoleRepository _userroleRepository;
		private readonly IUserRepository _userRepository;
		private readonly IAddressRepository _addressRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        /*public UsersController() : this(new UserRoleRepository(), new UserRepository())
        {
        }*/

		public UsersController(IUserRoleRepository userroleRepository, IUserRepository userRepository, IAddressRepository addressRepository)
        {
			this._userroleRepository = userroleRepository;
			this._userRepository = userRepository;
			this._addressRepository = addressRepository;
        }

        //
        // GET: /Users/
		[Authorize(Roles = "Admin, Editor")]
        public ViewResult Index()
        {
            return View(_userRepository.AllIncluding(user => user.Address));
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(int id)
        {
            return View(_userRepository.Find(id));
        }

        //
        // GET: /Users/Create
		[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
			ViewBag.PossibleUserRoles = _userroleRepository.All;

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
                _userRepository.InsertOrUpdate(user);
                _userRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUserRoles = _userroleRepository.All;
				return View(user);
			}
        }
        
        //
        // GET: /Users/Edit/5
		[HttpGet]
		[Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleUserRoles = _userroleRepository.All;
             return View(_userRepository.Find(id));
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
		[Authorize(Roles = "Admin")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid) {
                _userRepository.InsertOrUpdate(user);
                _userRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUserRoles = _userroleRepository.All;
				return View(user);
			}
        }

		[Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View(_userRepository.Find(id));
        }
		
        [HttpPost, ActionName("Delete")]
		[Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            _userRepository.Delete(id);
            _userRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _userroleRepository.Dispose();
                _userRepository.Dispose();
            }
            base.Dispose(disposing);
        }

		//
		// Show account for the current user
		//
		[Authorize]
		public ViewResult MyAccount()
		{
			//Get the current user 
			string userName = HttpContext.User.Identity.Name;
			User user = _userRepository.AllIncluding(u => u.Address).FirstOrDefault(u => u.Name == userName);
			return View(user);
		}
		[Authorize]
		public ActionResult EditUserInfo()
		{
			//Get the current user 
			string userName = HttpContext.User.Identity.Name;
			User user = _userRepository.All.FirstOrDefault(u => u.Name == userName);

			//Is user authenticated? returns true or false
			bool userAuth = HttpContext.User.Identity.IsAuthenticated;
			TempData.Remove("UserId");
			TempData.Remove("UserName");
			TempData.Remove("UserUserRoleId");
			TempData.Add("UserId", user.UserId);
			TempData.Add("UserName", user.Name);
			TempData.Add("UserUserRoleId", user.UserRoleId);

			return View(_userRepository.Find(user.UserId));
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
				_userRepository.InsertOrUpdate(user);
				_userRepository.Save();
				return RedirectToAction("MyAccount");
			}
			else
			{
				ViewBag.PossibleUserRoles = _userroleRepository.All;
				return View(user);
			}
		}
		[Authorize]
		public ActionResult EditAddress(int addressId)
		{
			Address address = _addressRepository.Find(addressId);

			//Check with the user Id
			string userName = HttpContext.User.Identity.Name;
			User user = _userRepository.All.FirstOrDefault(u => u.Name == userName);

			if (address.UserId != user.UserId) {
				return RedirectToAction("MyAccount");
			}

			return View(_addressRepository.Find(addressId));
		}
		[HttpPost]
		[Authorize]
		public ActionResult EditAddress(Address address)
		{
			if (ModelState.IsValid)
			{
				_addressRepository.InsertOrUpdate(address);
				_addressRepository.Save();
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
			User user = _userRepository.All.FirstOrDefault(u => u.Name == userName);

			address.UserId = user.UserId;
			ModelState["UserId"].Errors.Clear();

			if (ModelState.IsValid)
			{
				_addressRepository.InsertOrUpdate(address);
				_addressRepository.Save();
				return RedirectToAction("MyAccount");
			}
			else
			{
				return View(address);
			}
		}
		[Authorize]
		public ActionResult RemoveAddress(int addressId)
		{
			return View(_addressRepository.Find(addressId));
		}

		[HttpPost]
		[Authorize]
		public ActionResult RemoveAddressConfirmed(int addressId)
		{
			Address address = _addressRepository.Find(addressId);

			//Check with the user Id
			string userName = HttpContext.User.Identity.Name;
			User user = _userRepository.All.FirstOrDefault(u => u.Name == userName);

			if (address.UserId == user.UserId)
			{
				_addressRepository.Delete(addressId);
				_addressRepository.Save();
				return RedirectToAction("MyAccount");
			}
			else
			{
				return RedirectToAction("MyAccount");
			}
		}
    }
}

