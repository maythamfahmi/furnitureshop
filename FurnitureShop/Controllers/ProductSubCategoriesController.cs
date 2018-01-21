using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class ProductSubCategoriesController : Controller
    {
		private readonly IProductRepository _productRepository;
		private readonly ISubCategoryRepository _subcategoryRepository;
		private readonly IProductSubCategoryRepository _productsubcategoryRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ProductSubCategoriesController() : this(new ProductRepository(), new SubCategoryRepository(), new ProductSubCategoryRepository())
        {
        }

        public ProductSubCategoriesController(IProductRepository productRepository, ISubCategoryRepository subcategoryRepository, IProductSubCategoryRepository productsubcategoryRepository)
        {
			this._productRepository = productRepository;
			this._subcategoryRepository = subcategoryRepository;
			this._productsubcategoryRepository = productsubcategoryRepository;
        }

        //
        // GET: /ProductSubCategories/

        public ViewResult Index()
        {
            return View(_productsubcategoryRepository.AllIncluding(productsubcategory => productsubcategory.Product, productsubcategory => productsubcategory.SubCategory));
        }

        //
        // GET: /ProductSubCategories/Details/5

        public ViewResult Details(int id)
        {
            return View(_productsubcategoryRepository.Find(id));
        }

        //
        // GET: /ProductSubCategories/Create

        public ActionResult Create()
        {
			ViewBag.PossibleProducts = _productRepository.All;
			ViewBag.PossibleSubCategories = _subcategoryRepository.All;
            return View();
        } 

        //
        // POST: /ProductSubCategories/Create

        [HttpPost]
        public ActionResult Create(ProductSubCategory productsubcategory)
        {
            if (ModelState.IsValid) {
                _productsubcategoryRepository.InsertOrUpdate(productsubcategory);
                _productsubcategoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProducts = _productRepository.All;
				ViewBag.PossibleSubCategories = _subcategoryRepository.All;
				return View();
			}
        }
        
        //
        // GET: /ProductSubCategories/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleProducts = _productRepository.All;
			ViewBag.PossibleSubCategories = _subcategoryRepository.All;
             return View(_productsubcategoryRepository.Find(id));
        }

        //
        // POST: /ProductSubCategories/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductSubCategory productsubcategory)
        {
            if (ModelState.IsValid) {
                _productsubcategoryRepository.InsertOrUpdate(productsubcategory);
                _productsubcategoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProducts = _productRepository.All;
				ViewBag.PossibleSubCategories = _subcategoryRepository.All;
				return View();
			}
        }

        //
        // GET: /ProductSubCategories/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_productsubcategoryRepository.Find(id));
        }

        //
        // POST: /ProductSubCategories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _productsubcategoryRepository.Delete(id);
            _productsubcategoryRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _productRepository.Dispose();
                _subcategoryRepository.Dispose();
                _productsubcategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

