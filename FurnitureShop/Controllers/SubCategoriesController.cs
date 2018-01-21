using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class SubCategoriesController : Controller
    {
		private readonly ICategoryRepository _categoryRepository;
		private readonly ISubCategoryRepository _subcategoryRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        /*public SubCategoriesController() : this(new SubCategoryRepository())
        {
        }*/

		public SubCategoriesController(ISubCategoryRepository subcategoryRepository, ICategoryRepository categoryRepository)
        {
			this._subcategoryRepository = subcategoryRepository;
			this._categoryRepository = categoryRepository;
        }

        //
        // GET: /SubCategories/

        public ViewResult Index()
        {
            return View(_subcategoryRepository.All);
        }

        //
        // GET: /SubCategories/Details/5

        public ViewResult Details(int id)
        {
            return View(_subcategoryRepository.Find(id));
        }

        //
        // GET: /SubCategories/Create

        public ActionResult Create()
        {
			ViewBag.PossibleCategories = _categoryRepository.All;
            return View();
        } 

        //
        // POST: /SubCategories/Create

        [HttpPost]
        public ActionResult Create(SubCategory subcategory)
        {
            if (ModelState.IsValid) {
                _subcategoryRepository.InsertOrUpdate(subcategory);
                _subcategoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleCategories = _categoryRepository.All;
				return View();
			}
        }
        
        //
        // GET: /SubCategories/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleCategories = _categoryRepository.All;
             return View(_subcategoryRepository.Find(id));
        }

        //
        // POST: /SubCategories/Edit/5

        [HttpPost]
        public ActionResult Edit(SubCategory subcategory)
        {
            if (ModelState.IsValid) {
                _subcategoryRepository.InsertOrUpdate(subcategory);
                _subcategoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleCategories = _categoryRepository.All;
				return View();
			}
        }

        //
        // GET: /SubCategories/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_subcategoryRepository.Find(id));
        }

        //
        // POST: /SubCategories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _subcategoryRepository.Delete(id);
            _subcategoryRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _subcategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

