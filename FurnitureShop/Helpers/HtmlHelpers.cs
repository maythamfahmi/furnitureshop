using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Helpers
{
    public class Helpers
    {
        public class AddressesController : Controller
        {
            private readonly IUserRepository _userRepository;
            private readonly IAddressRepository _addressRepository;

            // If you are using Dependency Injection, you can delete the following constructor
            //public AddressesController()
            //    : this(new UserRepository(), new AddressRepository())
            //{
            //}

            public AddressesController(IUserRepository userRepository, IAddressRepository addressRepository)
            {
                this._userRepository = userRepository;
                this._addressRepository = addressRepository;
            }

            //
            // GET: /Addresses/

            public ViewResult Index()
            {
                return View(_addressRepository.All);
            }

            //
            // GET: /Addresses/Details/5

            public ViewResult Details(int id)
            {
                return View(_addressRepository.Find(id));
            }

            //
            // GET: /Addresses/Create

            public ActionResult Create()
            {
                ViewBag.PossibleUsers = _userRepository.All;
                return View();
            }

            //
            // POST: /Addresses/Create

            [HttpPost]
            public ActionResult Create(Address address)
            {
                if (ModelState.IsValid)
                {
                    _addressRepository.InsertOrUpdate(address);
                    _addressRepository.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.PossibleUsers = _userRepository.All;
                    return View();
                }
            }

            //
            // GET: /Addresses/Edit/5

            public ActionResult Edit(int id)
            {
                ViewBag.PossibleUsers = _userRepository.All;
                return View(_addressRepository.Find(id));
            }

            //
            // POST: /Addresses/Edit/5

            [HttpPost]
            public ActionResult Edit(Address address)
            {
                if (ModelState.IsValid)
                {
                    _addressRepository.InsertOrUpdate(address);
                    _addressRepository.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.PossibleUsers = _userRepository.All;
                    return View();
                }
            }

            //
            // GET: /Addresses/Delete/5

            public ActionResult Delete(int id)
            {
                return View(_addressRepository.Find(id));
            }

            //
            // POST: /Addresses/Delete/5

            [HttpPost, ActionName("Delete")]
            public ActionResult DeleteConfirmed(int id)
            {
                _addressRepository.Delete(id);
                _addressRepository.Save();

                return RedirectToAction("Index");
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _userRepository.Dispose();
                    _addressRepository.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }
}