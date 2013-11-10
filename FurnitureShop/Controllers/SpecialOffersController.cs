using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class SpecialOffersController : Controller
    {
		private readonly ISpecialOfferRepository specialofferRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public SpecialOffersController() : this(new SpecialOfferRepository())
        {
        }

        public SpecialOffersController(ISpecialOfferRepository specialofferRepository)
        {
			this.specialofferRepository = specialofferRepository;
        }

        //
        // GET: /SpecialOffers/

        public ViewResult Index()
        {
            return View(specialofferRepository.All);
        }

        //
        // GET: /SpecialOffers/Details/5

        public ViewResult Details(int id)
        {
            return View(specialofferRepository.Find(id));
        }

        //
        // GET: /SpecialOffers/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /SpecialOffers/Create

        [HttpPost]
        public ActionResult Create(SpecialOffer specialoffer)
        {
            if (ModelState.IsValid) {
                specialofferRepository.InsertOrUpdate(specialoffer);
                specialofferRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /SpecialOffers/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(specialofferRepository.Find(id));
        }

        //
        // POST: /SpecialOffers/Edit/5

        [HttpPost]
        public ActionResult Edit(SpecialOffer specialoffer)
        {
            if (ModelState.IsValid) {
                specialofferRepository.InsertOrUpdate(specialoffer);
                specialofferRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /SpecialOffers/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(specialofferRepository.Find(id));
        }

        //
        // POST: /SpecialOffers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            specialofferRepository.Delete(id);
            specialofferRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                specialofferRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

