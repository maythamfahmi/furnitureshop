using FurnitureShop.Models;
using FurnitureShop.Repository;
using System.Collections.Generic;
using System.Linq;
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

        private IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public NaviController(IProductRepository repo, ICategoryRepository catRepo)
        {
            _productRepository = repo;
            _categoryRepository = catRepo;
        }
        public PartialViewResult Menu(string category = null)
        {

            ViewBag.SelectedCategory = category;


            //IEnumerable<string> categories = categoryRepository.All.ToList().Select(c => c.Name).ToArray(); //productRepository.AllIncluding(product => product.Categories, product => product.SubCategories) //categoryRepository.All.ToList().Select(c => c.Name).ToArray(); /*productRepository.All

			List<Category> categories = _categoryRepository.All.ToList();

            //IEnumerable<string> categories = productRepository.All
            //    .Select(x => x.Category)
            //    .Distinct()
            //    .OrderBy(x => x);

            return PartialView(categories);

        }

		public PartialViewResult AdminMenu()
		{
			string isInRole = (HttpContext.User.IsInRole("Admin") || HttpContext.User.IsInRole("Editor"))? "True" : "False";

			return PartialView("AdminMenu", (string)isInRole);
		}
    }
}
