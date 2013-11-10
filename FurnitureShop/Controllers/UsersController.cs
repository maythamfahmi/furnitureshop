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

		// If you are using Dependency Injection, you can delete the following constructor
        public UsersController() : this(new UserRoleRepository(), new UserRepository())
        {
        }

        public UsersController(IUserRoleRepository userroleRepository, IUserRepository userRepository)
        {
			this.userroleRepository = userroleRepository;
			this.userRepository = userRepository;
        }

        //
        // GET: /Users/

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
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUserRoles = userroleRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleUserRoles = userroleRepository.All;
             return View(userRepository.Find(id));
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUserRoles = userroleRepository.All;
				return View();
			}
        }

        //
        // GET: /Users/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(userRepository.Find(id));
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
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
    }
}

