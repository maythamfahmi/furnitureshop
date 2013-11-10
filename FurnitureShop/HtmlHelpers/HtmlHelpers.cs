using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.HtmlHelpers
{
    public class HtmlHelpers
    {
        public class AddressesController : Controller
        {
            private readonly IUserRepository userRepository;
            private readonly IAddressRepository addressRepository;

            // If you are using Dependency Injection, you can delete the following constructor
            public AddressesController()
                : this(new UserRepository(), new AddressRepository())
            {
            }

            public AddressesController(IUserRepository userRepository, IAddressRepository addressRepository)
            {
                this.userRepository = userRepository;
                this.addressRepository = addressRepository;
            }

            //
            // GET: /Addresses/

            public ViewResult Index()
            {
                return View(addressRepository.All);
            }

            //
            // GET: /Addresses/Details/5

            public ViewResult Details(int id)
            {
                return View(addressRepository.Find(id));
            }

            //
            // GET: /Addresses/Create

            public ActionResult Create()
            {
                ViewBag.PossibleUsers = userRepository.All;
                return View();
            }

            //
            // POST: /Addresses/Create

            [HttpPost]
            public ActionResult Create(Address address)
            {
                if (ModelState.IsValid)
                {
                    addressRepository.InsertOrUpdate(address);
                    addressRepository.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.PossibleUsers = userRepository.All;
                    return View();
                }
            }

            //
            // GET: /Addresses/Edit/5

            public ActionResult Edit(int id)
            {
                ViewBag.PossibleUsers = userRepository.All;
                return View(addressRepository.Find(id));
            }

            //
            // POST: /Addresses/Edit/5

            [HttpPost]
            public ActionResult Edit(Address address)
            {
                if (ModelState.IsValid)
                {
                    addressRepository.InsertOrUpdate(address);
                    addressRepository.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.PossibleUsers = userRepository.All;
                    return View();
                }
            }

            //
            // GET: /Addresses/Delete/5

            public ActionResult Delete(int id)
            {
                return View(addressRepository.Find(id));
            }

            //
            // POST: /Addresses/Delete/5

            [HttpPost, ActionName("Delete")]
            public ActionResult DeleteConfirmed(int id)
            {
                addressRepository.Delete(id);
                addressRepository.Save();

                return RedirectToAction("Index");
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    userRepository.Dispose();
                    addressRepository.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }
}