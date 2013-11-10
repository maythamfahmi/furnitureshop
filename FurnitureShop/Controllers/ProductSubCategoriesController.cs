using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class ProductSubCategoriesController : Controller
    {
		private readonly IProductRepository productRepository;
		private readonly ISubCategoryRepository subcategoryRepository;
		private readonly IProductSubCategoryRepository productsubcategoryRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ProductSubCategoriesController() : this(new ProductRepository(), new SubCategoryRepository(), new ProductSubCategoryRepository())
        {
        }

        public ProductSubCategoriesController(IProductRepository productRepository, ISubCategoryRepository subcategoryRepository, IProductSubCategoryRepository productsubcategoryRepository)
        {
			this.productRepository = productRepository;
			this.subcategoryRepository = subcategoryRepository;
			this.productsubcategoryRepository = productsubcategoryRepository;
        }

        //
        // GET: /ProductSubCategories/

        public ViewResult Index()
        {
            return View(productsubcategoryRepository.AllIncluding(productsubcategory => productsubcategory.Product, productsubcategory => productsubcategory.SubCategory));
        }

        //
        // GET: /ProductSubCategories/Details/5

        public ViewResult Details(int id)
        {
            return View(productsubcategoryRepository.Find(id));
        }

        //
        // GET: /ProductSubCategories/Create

        public ActionResult Create()
        {
			ViewBag.PossibleProducts = productRepository.All;
			ViewBag.PossibleSubCategories = subcategoryRepository.All;
            return View();
        } 

        //
        // POST: /ProductSubCategories/Create

        [HttpPost]
        public ActionResult Create(ProductSubCategory productsubcategory)
        {
            if (ModelState.IsValid) {
                productsubcategoryRepository.InsertOrUpdate(productsubcategory);
                productsubcategoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProducts = productRepository.All;
				ViewBag.PossibleSubCategories = subcategoryRepository.All;
				return View();
			}
        }
        
        //
        // GET: /ProductSubCategories/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleProducts = productRepository.All;
			ViewBag.PossibleSubCategories = subcategoryRepository.All;
             return View(productsubcategoryRepository.Find(id));
        }

        //
        // POST: /ProductSubCategories/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductSubCategory productsubcategory)
        {
            if (ModelState.IsValid) {
                productsubcategoryRepository.InsertOrUpdate(productsubcategory);
                productsubcategoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProducts = productRepository.All;
				ViewBag.PossibleSubCategories = subcategoryRepository.All;
				return View();
			}
        }

        //
        // GET: /ProductSubCategories/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(productsubcategoryRepository.Find(id));
        }

        //
        // POST: /ProductSubCategories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            productsubcategoryRepository.Delete(id);
            productsubcategoryRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                productRepository.Dispose();
                subcategoryRepository.Dispose();
                productsubcategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

