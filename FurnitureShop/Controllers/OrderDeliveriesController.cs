using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class OrderDeliveriesController : Controller
    {
		private readonly IOrderDeliveryRepository _orderdeliveryRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderDeliveriesController() : this(new OrderDeliveryRepository())
        {
        }

        public OrderDeliveriesController(IOrderDeliveryRepository orderdeliveryRepository)
        {
			this._orderdeliveryRepository = orderdeliveryRepository;
        }

        //
        // GET: /OrderDeliveries/

        public ViewResult Index()
        {
            return View(_orderdeliveryRepository.All);
        }

        //
        // GET: /OrderDeliveries/Details/5

        public ViewResult Details(int id)
        {
            return View(_orderdeliveryRepository.Find(id));
        }

        //
        // GET: /OrderDeliveries/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /OrderDeliveries/Create

        [HttpPost]
        public ActionResult Create(OrderDelivery orderdelivery)
        {
            if (ModelState.IsValid) {
                _orderdeliveryRepository.InsertOrUpdate(orderdelivery);
                _orderdeliveryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /OrderDeliveries/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(_orderdeliveryRepository.Find(id));
        }

        //
        // POST: /OrderDeliveries/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderDelivery orderdelivery)
        {
            if (ModelState.IsValid) {
                _orderdeliveryRepository.InsertOrUpdate(orderdelivery);
                _orderdeliveryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /OrderDeliveries/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_orderdeliveryRepository.Find(id));
        }

        //
        // POST: /OrderDeliveries/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _orderdeliveryRepository.Delete(id);
            _orderdeliveryRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _orderdeliveryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

