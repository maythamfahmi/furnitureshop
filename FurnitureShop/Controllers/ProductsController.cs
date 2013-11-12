using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FurnitureShop.Models;
using FurnitureShop.Repository;
using FurnitureShop.Helpers;

namespace FurnitureShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
		private readonly ISubCategoryRepository subCategoryRepository;
		private readonly IProductSubCategoryRepository productSubCategoryRepository;

        public int PageSize = 4;
        // If you are using Dependency Injection, you can delete the following constructor
        //public ProductsController() : this(new CategoryRepository(), new ProductRepository())
        //{
        //}

		public ProductsController(ICategoryRepository categoryRepository, IProductRepository productRepository, ISubCategoryRepository subCategoryRepository, IProductSubCategoryRepository productSubCategoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
			this.subCategoryRepository = subCategoryRepository;
			this.productSubCategoryRepository = productSubCategoryRepository;
        }

        //
        // GET: /Products/

		
        public ViewResult Index()
        {
            return View(productRepository.AllIncluding(product => product.Category, product => product.SubCategories));
        }

        //
        // GET: /Products/Details/5

        public ViewResult Details(int id)
        {
            return View(productRepository.Find(id));
        }

        //
        // GET: /Products/Create

        public ActionResult Create()
        {
			//Create a list containing both selected and non-selected subcategories for the product
			List<SubCategory> SubCategoriesInSystem = subCategoryRepository.All.ToList();
			List<ItemCheckSelected> SubCategoriesAvailable = new List<ItemCheckSelected>();

			foreach (SubCategory subCategory in SubCategoriesInSystem)
			{
				SubCategoriesAvailable.Add(new ItemCheckSelected()
				{
					ItemId = subCategory.SubCategoryId,
					Name = subCategory.Name,
					Selected = false
				});
			}
			ViewBag.SubCategories = SubCategoriesAvailable;
            ViewBag.PossibleCategories = categoryRepository.All;
            return View();
        }

        //
        // POST: /Products/Create

        [HttpPost]
		public ActionResult Create(Product product, HttpPostedFileBase image, List<ItemCheckSelected> ProductSubCategory) // manually added HttpPostedFileBase image
        {
            if (ModelState.IsValid)
            {
                // manually added 
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                // manually added end

                productRepository.InsertOrUpdate(product);
                productRepository.Save();

				//Add new connections
				foreach (ItemCheckSelected SubCatSelected in ProductSubCategory.FindAll(c => c.Selected == true))
				{
					ProductSubCategory SubCatConnection = new ProductSubCategory()
					{
						ProductId = product.ProductId,
						SubCategoryId = SubCatSelected.ItemId
					};
					productSubCategoryRepository.InsertOrUpdate(SubCatConnection);
				}
				productSubCategoryRepository.Save();

                return RedirectToAction("Index");
            }
            else
            {
				ViewBag.SubCategories = ProductSubCategory;
                ViewBag.PossibleCategories = categoryRepository.All;
                return View();
            }
        }

        //
        // GET: /Products/Edit/5

        public ActionResult Edit(int id)
        {
			Product product = productRepository.Find(id);
			//Create a list containing both selected and non-selected subcategories for the product
			List<SubCategory> SubCategoriesInSystem = subCategoryRepository.All.ToList();
			List<SubCategory> ProductSubCategoriesSelected = product.SubCategories.Select(c => c.SubCategory).ToList();
			List<ItemCheckSelected> SubCategoriesAvailable = new List<ItemCheckSelected>();

			foreach (SubCategory subCategory in SubCategoriesInSystem)
			{
				SubCategoriesAvailable.Add(new ItemCheckSelected()
				{
					ItemId = subCategory.SubCategoryId,
					Name = subCategory.Name,
					Selected = (ProductSubCategoriesSelected.FirstOrDefault(c => c.SubCategoryId == subCategory.SubCategoryId) != null) ? true : false
				});
			}
			ViewBag.SubCategories = SubCategoriesAvailable;
            ViewBag.PossibleCategories = categoryRepository.All;
            return View(productRepository.Find(id));
        }

        //
        // POST: /Products/Edit/5

        [HttpPost]
		public ActionResult Edit(Product product, HttpPostedFileBase image, List<ItemCheckSelected> ProductSubCategory) // manually added HttpPostedFileBase image
        {
            if (ModelState.IsValid)
            {
				//Remove all existing ProductSubcategory entries for this product
				List<ProductSubCategory> SubCatConnections = productSubCategoryRepository.All.ToList().FindAll(s => s.ProductId == product.ProductId).ToList();

				//productSubCategoryRepository.All.ToList().RemoveAll(s => s.ProductId == product.ProductId);

				foreach (ProductSubCategory SubCatConnection in SubCatConnections)
				{
					//context.ProductSubCategories.Remove(SubCatConnection);
					productSubCategoryRepository.Delete(SubCatConnection.ProductSubCategoryId);
				}
				//productSubCategoryRepository.Save();

				//Add new connections
				foreach (ItemCheckSelected SubCatSelected in ProductSubCategory.FindAll(c => c.Selected == true))
				{
					ProductSubCategory SubCatConnection = new ProductSubCategory()
					{
						ProductId = product.ProductId,
						SubCategoryId = SubCatSelected.ItemId
					};
					productSubCategoryRepository.InsertOrUpdate(SubCatConnection);
					//context.ProductSubCategories.Add(SubCatConnection);
				}
				productSubCategoryRepository.Save();
                // manually added 
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                // manually added end

                productRepository.InsertOrUpdate(product);
                productRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
				ViewBag.SubCategories = ProductSubCategory;
                ViewBag.PossibleCategories = categoryRepository.All;
                return View();
            }
        }

        //
        // GET: /Products/Delete/5

        public ActionResult Delete(int id)
        {
            return View(productRepository.Find(id));
        }

        //
        // POST: /Products/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            productRepository.Delete(id);
            productRepository.Save();

            return RedirectToAction("Index");
        }

        //public ActionResult List()
        //{
        //    return View(productRepository.All //Including(product => product.Categories, product => product.SubCategories));
        //        .OrderBy(p => p.ProductId)
        //        .Skip((PageSize - 1) * PageSize)
        //        .Take(PageSize));
        //}

        public ViewResult List(string category, int page = 1)
        {
            ProductListView model = new ProductListView
            {
                Products = productRepository.AllIncluding(product => product.Category, product => product.SubCategories)
                .Where(p => category == null) // || p.Categories == category)
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = productRepository.All.Count()
                    //TotalItems = category == null ?
                    //    productRepository.All.Count() :
                    //    productRepository.All.Where(e => e.Categories == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public FileContentResult GetImage(int id)
        {
            Product prod = productRepository.All.FirstOrDefault(p => p.ProductId == id);

            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                categoryRepository.Dispose();
                productRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

