using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class SpecialOffersController : Controller
    {
		private readonly ISpecialOfferRepository _specialofferRepository;
        private readonly IProductRepository _productRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        //public SpecialOffersController() : this(new SpecialOfferRepository())
        //{
        //}

        public SpecialOffersController(
            ISpecialOfferRepository specialofferRepository,
            IProductRepository productRepository)
        {
			this._specialofferRepository = specialofferRepository;
            this._productRepository = productRepository;
        }

        //
        // GET: /SpecialOffers/

        public ViewResult Index()
        {
            return View(_specialofferRepository.All);
        }

        //
        // GET: /SpecialOffers/Details/5

        public ViewResult Details(int id)
        {
            return View(_specialofferRepository.Find(id));
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
            ViewBag.PossibleProducts = _productRepository.All;
            ViewBag.PossibleSProducts = _specialofferRepository.All;

            if (ModelState.IsValid) {
                _specialofferRepository.InsertOrUpdate(specialoffer);
                _specialofferRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /SpecialOffers/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(_specialofferRepository.Find(id));
        }

        //
        // POST: /SpecialOffers/Edit/5

        [HttpPost]
        public ActionResult Edit(SpecialOffer specialoffer)
        {
            if (ModelState.IsValid) {
                _specialofferRepository.InsertOrUpdate(specialoffer);
                _specialofferRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /SpecialOffers/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_specialofferRepository.Find(id));
        }

        //
        // POST: /SpecialOffers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _specialofferRepository.Delete(id);
            _specialofferRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _specialofferRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

