using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class OrderProductsController : Controller
    {
		private readonly IOrderRepository orderRepository;
		private readonly IOrderProductRepository orderproductRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderProductsController() : this(new OrderRepository(), new OrderProductRepository())
        {
        }

        public OrderProductsController(IOrderRepository orderRepository, IOrderProductRepository orderproductRepository)
        {
			this.orderRepository = orderRepository;
			this.orderproductRepository = orderproductRepository;
        }

        //
        // GET: /OrderProducts/

        public ViewResult Index()
        {
            return View(orderproductRepository.All);
        }

        //
        // GET: /OrderProducts/Details/5

        public ViewResult Details(int id)
        {
            return View(orderproductRepository.Find(id));
        }

        //
        // GET: /OrderProducts/Create

        public ActionResult Create()
        {
			ViewBag.PossibleOrders = orderRepository.All;
            return View();
        } 

        //
        // POST: /OrderProducts/Create

        [HttpPost]
        public ActionResult Create(OrderProduct orderproduct)
        {
            if (ModelState.IsValid) {
                orderproductRepository.InsertOrUpdate(orderproduct);
                orderproductRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleOrders = orderRepository.All;
				return View();
			}
        }
        
        //
        // GET: /OrderProducts/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleOrders = orderRepository.All;
             return View(orderproductRepository.Find(id));
        }

        //
        // POST: /OrderProducts/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderProduct orderproduct)
        {
            if (ModelState.IsValid) {
                orderproductRepository.InsertOrUpdate(orderproduct);
                orderproductRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleOrders = orderRepository.All;
				return View();
			}
        }

        //
        // GET: /OrderProducts/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(orderproductRepository.Find(id));
        }

        //
        // POST: /OrderProducts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            orderproductRepository.Delete(id);
            orderproductRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                orderRepository.Dispose();
                orderproductRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

