using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class SubCategoriesController : Controller
    {
		private readonly ISubCategoryRepository subcategoryRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public SubCategoriesController() : this(new SubCategoryRepository())
        {
        }

        public SubCategoriesController(ISubCategoryRepository subcategoryRepository)
        {
			this.subcategoryRepository = subcategoryRepository;
        }

        //
        // GET: /SubCategories/

        public ViewResult Index()
        {
            return View(subcategoryRepository.All);
        }

        //
        // GET: /SubCategories/Details/5

        public ViewResult Details(int id)
        {
            return View(subcategoryRepository.Find(id));
        }

        //
        // GET: /SubCategories/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /SubCategories/Create

        [HttpPost]
        public ActionResult Create(SubCategory subcategory)
        {
            if (ModelState.IsValid) {
                subcategoryRepository.InsertOrUpdate(subcategory);
                subcategoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /SubCategories/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(subcategoryRepository.Find(id));
        }

        //
        // POST: /SubCategories/Edit/5

        [HttpPost]
        public ActionResult Edit(SubCategory subcategory)
        {
            if (ModelState.IsValid) {
                subcategoryRepository.InsertOrUpdate(subcategory);
                subcategoryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /SubCategories/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(subcategoryRepository.Find(id));
        }

        //
        // POST: /SubCategories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            subcategoryRepository.Delete(id);
            subcategoryRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                subcategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

