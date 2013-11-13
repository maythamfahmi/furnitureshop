using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
	//[Authorize]
    public class UserRolesController : Controller
    {
		private readonly IUserRoleRepository userroleRepository;
		private readonly IUserRepository userRepository;

        public UserRolesController(IUserRoleRepository userroleRepository, IUserRepository userRepository)
        {
			this.userroleRepository = userroleRepository;
			this.userRepository = userRepository;
        }

        //
        // GET: /UserRoles/

        public ViewResult Index()
        {
            return View(userroleRepository.All);
        }

        //
        // GET: /UserRoles/Details/5

        public ViewResult Details(int id)
        {
            return View(userroleRepository.Find(id));
        }

        //
        // GET: /UserRoles/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /UserRoles/Create

        [HttpPost]
        public ActionResult Create(UserRole userrole)
        {
            if (ModelState.IsValid) {
                userroleRepository.InsertOrUpdate(userrole);
                userroleRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /UserRoles/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(userroleRepository.Find(id));
        }

        //
        // POST: /UserRoles/Edit/5

        [HttpPost]
        public ActionResult Edit(UserRole userrole)
        {
            if (ModelState.IsValid) {
                userroleRepository.InsertOrUpdate(userrole);
                userroleRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /UserRoles/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(userroleRepository.Find(id));
        }

        //
        // POST: /UserRoles/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            userroleRepository.Delete(id);
            userroleRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                userroleRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

