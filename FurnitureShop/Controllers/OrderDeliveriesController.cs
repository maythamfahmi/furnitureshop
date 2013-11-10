using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class OrderDeliveriesController : Controller
    {
		private readonly IOrderDeliveryRepository orderdeliveryRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderDeliveriesController() : this(new OrderDeliveryRepository())
        {
        }

        public OrderDeliveriesController(IOrderDeliveryRepository orderdeliveryRepository)
        {
			this.orderdeliveryRepository = orderdeliveryRepository;
        }

        //
        // GET: /OrderDeliveries/

        public ViewResult Index()
        {
            return View(orderdeliveryRepository.All);
        }

        //
        // GET: /OrderDeliveries/Details/5

        public ViewResult Details(int id)
        {
            return View(orderdeliveryRepository.Find(id));
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
                orderdeliveryRepository.InsertOrUpdate(orderdelivery);
                orderdeliveryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /OrderDeliveries/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(orderdeliveryRepository.Find(id));
        }

        //
        // POST: /OrderDeliveries/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderDelivery orderdelivery)
        {
            if (ModelState.IsValid) {
                orderdeliveryRepository.InsertOrUpdate(orderdelivery);
                orderdeliveryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /OrderDeliveries/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(orderdeliveryRepository.Find(id));
        }

        //
        // POST: /OrderDeliveries/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            orderdeliveryRepository.Delete(id);
            orderdeliveryRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                orderdeliveryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

