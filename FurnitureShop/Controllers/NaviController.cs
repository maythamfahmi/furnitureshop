using FurnitureShop.Models;
using FurnitureShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureShop.Controllers
{
    public class NaviController : Controller
    {
        //
        // GET: /Navi/

        //public string Menu()
        //{
        //    return "Hello from NavController";
        //}

        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;
        public NaviController(IProductRepository repo, ICategoryRepository catRepo)
        {
            productRepository = repo;
            categoryRepository = catRepo;
        }
        public PartialViewResult Menu(string category = null)
        {

            ViewBag.SelectedCategory = category;


            //IEnumerable<string> categories = categoryRepository.All.ToList().Select(c => c.Name).ToArray(); //productRepository.AllIncluding(product => product.Categories, product => product.SubCategories) //categoryRepository.All.ToList().Select(c => c.Name).ToArray(); /*productRepository.All

			List<Category> categories = categoryRepository.All.ToList();

            //IEnumerable<string> categories = productRepository.All
            //    .Select(x => x.Category)
            //    .Distinct()
            //    .OrderBy(x => x);

            return PartialView(categories);

        }
    }
}
