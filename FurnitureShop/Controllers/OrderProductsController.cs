using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class OrderProductsController : Controller
    {
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderProductRepository _orderproductRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public OrderProductsController() : this(new OrderRepository(), new OrderProductRepository())
        {
        }

        public OrderProductsController(IOrderRepository orderRepository, IOrderProductRepository orderproductRepository)
        {
			this._orderRepository = orderRepository;
			this._orderproductRepository = orderproductRepository;
        }

        //
        // GET: /OrderProducts/

        public ViewResult Index()
        {
            return View(_orderproductRepository.All);
        }

        //
        // GET: /OrderProducts/Details/5

        public ViewResult Details(int id)
        {
            return View(_orderproductRepository.Find(id));
        }

        //
        // GET: /OrderProducts/Create

        public ActionResult Create()
        {
			ViewBag.PossibleOrders = _orderRepository.All;
            return View();
        } 

        //
        // POST: /OrderProducts/Create

        [HttpPost]
        public ActionResult Create(OrderProduct orderproduct)
        {
            if (ModelState.IsValid) {
                _orderproductRepository.InsertOrUpdate(orderproduct);
                _orderproductRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleOrders = _orderRepository.All;
				return View();
			}
        }
        
        //
        // GET: /OrderProducts/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleOrders = _orderRepository.All;
             return View(_orderproductRepository.Find(id));
        }

        //
        // POST: /OrderProducts/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderProduct orderproduct)
        {
            if (ModelState.IsValid) {
                _orderproductRepository.InsertOrUpdate(orderproduct);
                _orderproductRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleOrders = _orderRepository.All;
				return View();
			}
        }

        //
        // GET: /OrderProducts/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_orderproductRepository.Find(id));
        }

        //
        // POST: /OrderProducts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _orderproductRepository.Delete(id);
            _orderproductRepository.Save();

            return RedirectToAction("Index");
        }

        public ActionResult CreateOrderProducts()
        {
            ViewBag.PossibleOrders = _orderRepository.All;
            return View();
        }

        //
        // POST: /OrderProducts/Create

        [HttpPost]
        public ActionResult CreateOrderProducts(OrderProduct orderproduct)
        {
            if (ModelState.IsValid)
            {
                _orderproductRepository.InsertOrUpdate(orderproduct);
                _orderproductRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.PossibleOrders = _orderRepository.All;
                return View();
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _orderRepository.Dispose();
                _orderproductRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

