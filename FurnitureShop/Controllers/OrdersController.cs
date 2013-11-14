using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IOrderDeliveryRepository orderdeliveryRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderProductRepository orderproductRepository;
        private readonly IAddressRepository addressRepository;

        //// If you are using Dependency Injection, you can delete the following constructor
        //public OrdersController()
        //    : this(new UserRepository(), new OrderDeliveryRepository(), new OrderRepository(), new OrderProductRepository())
        //{
        //}

        public OrdersController(
            IUserRepository userRepository,
            IOrderDeliveryRepository orderdeliveryRepository,
            IOrderRepository orderRepository,
            IOrderProductRepository orderproductRepository,
            IAddressRepository addressRepository
            )
        {
            this.userRepository = userRepository;
            this.orderdeliveryRepository = orderdeliveryRepository;
            this.orderRepository = orderRepository;
            this.orderproductRepository = orderproductRepository;
            this.addressRepository = addressRepository;
        }

        //
        // GET: /Orders/

        public ViewResult Index()
        {
            return View(orderRepository.All);
        }

        //
        // GET: /Orders/Details/5

        public ViewResult Details(int id)
        {
            return View(orderRepository.Find(id));
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            ViewBag.PossibleUsers = userRepository.All;
            //ViewBag.SelectedUser = userRepository.All.ToList().FirstOrDefault(o => o.UserId == 1);
            ViewBag.PossibleOrderDeliveries = orderdeliveryRepository.All;
            return View();
        }

        //
        // POST: /Orders/Create

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                orderRepository.InsertOrUpdate(order);
                orderRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.PossibleUsers = userRepository.All;
                ViewBag.PossibleOrderDeliveries = orderdeliveryRepository.All;
                return View();
            }
        }

        //
        // GET: /Orders/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleUsers = userRepository.All;
            ViewBag.PossibleOrderDeliveries = orderdeliveryRepository.All;
            return View(orderRepository.Find(id));
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                orderRepository.InsertOrUpdate(order);
                orderRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.PossibleUsers = userRepository.All;
                ViewBag.PossibleOrderDeliveries = orderdeliveryRepository.All;
                return View();
            }
        }

        //
        // GET: /Orders/Delete/5

        public ActionResult Delete(int id)
        {
            return View(orderRepository.Find(id));
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            orderRepository.Delete(id);
            orderRepository.Save();

            return RedirectToAction("Index");
        }

        public ViewResult CustomerOrder()
        {
            //ViewBag.Selected = userRepository.All.ToList().FirstOrDefault(o => o.UserId == 3);
            //ViewBag.SelectedOrder = orderRepository.All.ToList().FirstOrDefault(o => o.UserId == 2);


            string userName = HttpContext.User.Identity.Name;
            User user = userRepository.All.FirstOrDefault(u => u.Name == userName);
            List<Order> order = orderRepository.All.ToList().FindAll(o => o.UserId == user.UserId);

            //return View(orderRepository.All);
            return View(order);
            //ViewBag.SelecedUser = userRepository.All.FirstOrDefault(u => u.Name == userName);
            // //ViewBag.Selected = userRepository.All.ToList().FirstOrDefault(o => o.Name == userName);
            //ViewBag.SelectedOrderDeliveries = orderdeliveryRepository.All.ToList().Find(o => o.OrderDeliveryId == 1);
        }

        public ViewResult CustomerOrderProducts(int id, int userid)
        {
            ViewBag.SelectedUser = userRepository.All.ToList().FirstOrDefault(o => o.UserId == 1);
            ViewBag.SelectedAddress = addressRepository.All.ToList().FirstOrDefault(o => o.UserId == 1);
            ViewBag.SelectedOrders = orderRepository.All;
            ViewBag.SelectedOrderProducts = orderproductRepository.All.ToList().FindAll(o => o.OrderId == id);
            ViewBag.SelectedOrderDeliveries = orderdeliveryRepository.All;

            ViewBag.totalPrice = orderproductRepository.All.ToList().FindAll(o => o.OrderId == id).Sum(p => p.OProdcutPrice * p.OProdcutQty);
            return View(orderRepository.Find(id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userRepository.Dispose();
                orderdeliveryRepository.Dispose();
                orderRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

