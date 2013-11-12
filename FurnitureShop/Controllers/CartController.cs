using FurnitureShop.Interface;
using FurnitureShop.Models;
using FurnitureShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureShop.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository productRepository;
        private IOrderProcessor orderProcessor;
        private IUserRepository userRepository;
        private IAddressRepository addressRepository;
        public CartController(
            IProductRepository productRepository,
            IOrderProcessor orderProcessor,
            IUserRepository userRepository,
            IAddressRepository addressRepository)
        {
            this.productRepository = productRepository;
            this.orderProcessor = orderProcessor;
            this.userRepository = userRepository;
            this.addressRepository = addressRepository;
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
            Product product = productRepository.All
            .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.AddItem(product, 1); //GetCart()
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = productRepository.All
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
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public ViewResult Checkout()
        {
            ViewBag.SelectedUser = userRepository.All.ToList().FirstOrDefault(o => o.UserId == 1);
            ViewBag.SelectedAddress = addressRepository.All.ToList().FirstOrDefault(o => o.UserId == 1);
            return View(new ShippingDetails());
        }

    }
}
