using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
	//[Authorize]
    public class UserRolesController : Controller
    {
		private readonly IUserRoleRepository _userroleRepository;
		private readonly IUserRepository _userRepository;

        public UserRolesController(IUserRoleRepository userroleRepository, IUserRepository userRepository)
        {
			this._userroleRepository = userroleRepository;
			this._userRepository = userRepository;
        }

        //
        // GET: /UserRoles/

        public ViewResult Index()
        {
            return View(_userroleRepository.All);
        }

        //
        // GET: /UserRoles/Details/5

        public ViewResult Details(int id)
        {
            return View(_userroleRepository.Find(id));
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
                _userroleRepository.InsertOrUpdate(userrole);
                _userroleRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /UserRoles/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(_userroleRepository.Find(id));
        }

        //
        // POST: /UserRoles/Edit/5

        [HttpPost]
        public ActionResult Edit(UserRole userrole)
        {
            if (ModelState.IsValid) {
                _userroleRepository.InsertOrUpdate(userrole);
                _userroleRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /UserRoles/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_userroleRepository.Find(id));
        }

        //
        // POST: /UserRoles/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _userroleRepository.Delete(id);
            _userroleRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _userroleRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

