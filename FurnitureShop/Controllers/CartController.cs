using FurnitureShop.Interface;
using FurnitureShop.Models;
using FurnitureShop.Repository;
using System.Linq;
using System.Web.Mvc;

namespace FurnitureShop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderProcessor _orderProcessor;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderproductRepository;
        private readonly IOrderDeliveryRepository _orderdeliveryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        public CartController(
            IProductRepository productRepository,
            IOrderProcessor orderProcessor,
            IOrderRepository orderRepository,
            IOrderProductRepository orderproductRepository,
            IOrderDeliveryRepository orderdeliveryRepository,
            IUserRepository userRepository,
            IAddressRepository addressRepository
            )
        {
            this._productRepository = productRepository;
            this._orderProcessor = orderProcessor;
            this._orderRepository = orderRepository;
            this._orderproductRepository = orderproductRepository;
            this._orderdeliveryRepository = orderdeliveryRepository;
            this._userRepository = userRepository;
            this._addressRepository = addressRepository;
        }

        // new content added
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexView
            {
                Cart = cart, //GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _productRepository.All
            .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.AddItem(product, 1); //GetCart()
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveItemFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _productRepository.All
            .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.RemoveItem(product, 1); //GetCart()
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _productRepository.All
            .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.RemoveLine(product); //GetCart()
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public PartialViewResult Summary(Cart cart)
        {
            //if (cart ==)
            //{ return null; }
            return PartialView(cart);
        }

        [HttpPost]
        [Authorize]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails, Order order, OrderProduct orderproduct)
        {

            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(cart, shippingDetails);

                // create new order // the 2 auth line should be disbaled in case error fri test
                string userName = HttpContext.User.Identity.Name;
                User user = _userRepository.All.FirstOrDefault(u => u.Name == userName);
                order.UserId = (int)user.UserId;
                order.OrderDate = System.DateTime.Now; // Today;
                order.OrderDeliveryId = order.OrderDeliveryId; // henter shipping info fra dropdown
                _orderRepository.InsertOrUpdate(order);
                _orderRepository.Save();
                //

                // save cart contents to order products
                foreach (var line in cart.Lines)
                {
                    orderproduct.OrderId = order.OrderId;
                    orderproduct.OProdcutId = line.Product.ProductId;
                    orderproduct.OProdcutName = line.Product.Name;
                    orderproduct.OProdcutQty = line.Quantity;
                    orderproduct.OProdcutPrice = (int)line.Product.Price;
                    _orderproductRepository.InsertOrUpdate(orderproduct);
                    _orderproductRepository.Save();
                }
                //

                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        [Authorize]
        public ViewResult Checkout()
        {
            string userName = HttpContext.User.Identity.Name;
            User user = _userRepository.All.FirstOrDefault(u => u.Name == userName); //"maytham"); //userName
            Address address = _addressRepository.All.FirstOrDefault(u => u.UserId == user.UserId); //"maytham"); //userName
            //TempData.Remove("UserFirstName");
            //TempData.Add("UserFirstName", user.FirstName);
            //ViewBag.userName = (string)TempData["UserName"];
            ViewBag.FullName = (string)user.FirstName + " " + (string)user.LastName;
            ViewBag.AddressLine1 = (string)address.AddressLine1;
            //ViewBag.Line1         = (string)address.AddressLine1;
            //ViewBag.Line1         = (string)address.AddressLine1;
            ViewBag.City = (string)address.City;
            ViewBag.Postal = (string)address.Postal;
            ViewBag.Country = (string)address.Country;
            ViewBag.PossibleOrderDeliveries = _orderdeliveryRepository.All;

            //ViewBag.SelectedUser = userRepository.All.ToList().FirstOrDefault(o => o.UserId == 1);
            //ViewBag.SelectedAddress = addressRepository.All.ToList().FirstOrDefault(o => o.UserId == 1);
            return View(new ShippingDetails());
        }

    }
}
