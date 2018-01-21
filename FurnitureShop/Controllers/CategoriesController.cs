using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class CategoriesController : Controller
    {
		private readonly ICategoryRepository _categoryRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public CategoriesController() : this(new CategoryRepository())
        {
        }

        public CategoriesController(ICategoryRepository categoryRepository)
        {
			this._categoryRepository = categoryRepository;
        }

        //
        // GET: /Categories/

        public ViewResult Index()
        {
            return View(_categoryRepository.All);
        }

        //
        // GET: /Categories/Details/5

        public ViewResult Details(int id)
        {
            return View(_categoryRepository.Find(id));
        }

        //
        // GET: /Categories/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Categories/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid) {
                _categoryRepository.InsertOrUpdate(category);
                _categoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Categories/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(_categoryRepository.Find(id));
        }

        //
        // POST: /Categories/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid) {
                _categoryRepository.InsertOrUpdate(category);
                _categoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Categories/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_categoryRepository.Find(id));
        }

        //
        // POST: /Categories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoryRepository.Delete(id);
            _categoryRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _categoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

