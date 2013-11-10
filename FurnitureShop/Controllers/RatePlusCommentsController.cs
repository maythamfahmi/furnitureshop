using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;

namespace FurnitureShop.Controllers
{   
    public class RatePlusCommentsController : Controller
    {
		private readonly IProductRepository productRepository;
		private readonly IUserRepository userRepository;
		private readonly IRatePlusCommentRepository ratepluscommentRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public RatePlusCommentsController() : this(new ProductRepository(), new UserRepository(), new RatePlusCommentRepository())
        {
        }

        public RatePlusCommentsController(IProductRepository productRepository, IUserRepository userRepository, IRatePlusCommentRepository ratepluscommentRepository)
        {
			this.productRepository = productRepository;
			this.userRepository = userRepository;
			this.ratepluscommentRepository = ratepluscommentRepository;
        }

        //
        // GET: /RatePlusComments/

        public ViewResult Index()
        {
            return View(ratepluscommentRepository.All);
        }

        //
        // GET: /RatePlusComments/Details/5

        public ViewResult Details(int id)
        {
            return View(ratepluscommentRepository.Find(id));
        }

        //
        // GET: /RatePlusComments/Create

        public ActionResult Create()
        {
			ViewBag.PossibleProducts = productRepository.All;
			ViewBag.PossibleUsers = userRepository.All;
            return View();
        } 

        //
        // POST: /RatePlusComments/Create

        [HttpPost]
        public ActionResult Create(RatePlusComment ratepluscomment)
        {
            if (ModelState.IsValid) {
                ratepluscommentRepository.InsertOrUpdate(ratepluscomment);
                ratepluscommentRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProducts = productRepository.All;
				ViewBag.PossibleUsers = userRepository.All;
				return View();
			}
        }
        
        //
        // GET: /RatePlusComments/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleProducts = productRepository.All;
			ViewBag.PossibleUsers = userRepository.All;
             return View(ratepluscommentRepository.Find(id));
        }

        //
        // POST: /RatePlusComments/Edit/5

        [HttpPost]
        public ActionResult Edit(RatePlusComment ratepluscomment)
        {
            if (ModelState.IsValid) {
                ratepluscommentRepository.InsertOrUpdate(ratepluscomment);
                ratepluscommentRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleProducts = productRepository.All;
				ViewBag.PossibleUsers = userRepository.All;
				return View();
			}
        }

        //
        // GET: /RatePlusComments/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(ratepluscommentRepository.Find(id));
        }

        //
        // POST: /RatePlusComments/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ratepluscommentRepository.Delete(id);
            ratepluscommentRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                productRepository.Dispose();
                userRepository.Dispose();
                ratepluscommentRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

