using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderDeliveryRepository _orderdeliveryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderproductRepository;
        private readonly IAddressRepository _addressRepository;

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
            this._userRepository = userRepository;
            this._orderdeliveryRepository = orderdeliveryRepository;
            this._orderRepository = orderRepository;
            this._orderproductRepository = orderproductRepository;
            this._addressRepository = addressRepository;
        }

        //
        // GET: /Orders/

        public ViewResult Index()
        {
            return View(_orderRepository.All);
        }

        //
        // GET: /Orders/Details/5

        public ViewResult Details(int id)
        {
            return View(_orderRepository.Find(id));
        }

        //
        // GET: /Orders/Create

        public ActionResult Create()
        {
            ViewBag.PossibleUsers = _userRepository.All;
            //ViewBag.SelectedUser = userRepository.All.ToList().FirstOrDefault(o => o.UserId == 1);
            ViewBag.PossibleOrderDeliveries = _orderdeliveryRepository.All;
            return View();
        }

        //
        // POST: /Orders/Create

        [HttpPost]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.InsertOrUpdate(order);
                _orderRepository.Save();
                return RedirectToAction("CustomerOrder");
            }
            else
            {
                ViewBag.PossibleUsers = _userRepository.All;
                ViewBag.PossibleOrderDeliveries = _orderdeliveryRepository.All;
                return View();
            }
        }

        //
        // GET: /Orders/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleUsers = _userRepository.All;
            ViewBag.PossibleOrderDeliveries = _orderdeliveryRepository.All;
            return View(_orderRepository.Find(id));
        }

        //
        // POST: /Orders/Edit/5

        [HttpPost]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.InsertOrUpdate(order);
                _orderRepository.Save();
                return RedirectToAction("CustomerOrder");
            }
            else
            {
                ViewBag.PossibleUsers = _userRepository.All;
                ViewBag.PossibleOrderDeliveries = _orderdeliveryRepository.All;
                return View();
            }
        }

        //
        // GET: /Orders/Delete/5

        public ActionResult Delete(int id)
        {
            return View(_orderRepository.Find(id));
        }

        //
        // POST: /Orders/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _orderRepository.Delete(id);
            _orderRepository.Save();

            return RedirectToAction("CustomerOrder");
        }

        [Authorize]
        public ViewResult CustomerOrder()
        {
            //ViewBag.Selected = userRepository.All.ToList().FirstOrDefault(o => o.UserId == 3);
            //ViewBag.SelectedOrder = orderRepository.All.ToList().FirstOrDefault(o => o.UserId == 2);

            string userName = HttpContext.User.Identity.Name;
            User user = _userRepository.All.FirstOrDefault(u => u.Name == userName);

            if (userName == "admin")
            {
                List<Order> order = _orderRepository.All.ToList(); //.FindAll(o => o.UserId == user.UserId);
                return View(order);
            }
            else
            {
                List<Order> order = _orderRepository.All.ToList().FindAll(o => o.UserId == user.UserId);
                return View(order);
            }

            //return View(orderRepository.All);
            //return View(order);
            //ViewBag.SelecedUser = userRepository.All.FirstOrDefault(u => u.Name == userName);
            // //ViewBag.Selected = userRepository.All.ToList().FirstOrDefault(o => o.Name == userName);
            //ViewBag.SelectedOrderDeliveries = orderdeliveryRepository.All.ToList().Find(o => o.OrderDeliveryId == 1);
        }

        public ViewResult CustomerOrderProducts(int id, int userid)
        {
            ViewBag.SelectedUser = _userRepository.All.ToList().FirstOrDefault(o => o.UserId == 1);
            ViewBag.SelectedAddress = _addressRepository.All.ToList().FirstOrDefault(o => o.UserId == 1);
            ViewBag.SelectedOrders = _orderRepository.All;
            ViewBag.SelectedOrderProducts = _orderproductRepository.All.ToList().FindAll(o => o.OrderId == id);
            ViewBag.SelectedOrderDeliveries = _orderdeliveryRepository.All;

            ViewBag.totalPrice = _orderproductRepository.All.ToList().FindAll(o => o.OrderId == id).Sum(p => p.OProdcutPrice * p.OProdcutQty);
            return View(_orderRepository.Find(id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userRepository.Dispose();
                _orderdeliveryRepository.Dispose();
                _orderRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

