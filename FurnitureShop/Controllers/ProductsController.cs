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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IProductSubCategoryRepository _productSubCategoryRepository;
        private readonly IRatePlusCommentRepository _ratePlusCommentRepository;
        private readonly IUserRepository _userRepository;

        public int PageSize = 10;
        // If you are using Dependency Injection, you can delete the following constructor
        //public ProductsController() : this(new CategoryRepository(), new ProductRepository())
        //{
        //}

        public ProductsController(
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            ISubCategoryRepository subCategoryRepository,
            IProductSubCategoryRepository productSubCategoryRepository,
            IRatePlusCommentRepository ratePlusCommentRepository,
            IUserRepository userRepository
            )
        {
            this._categoryRepository = categoryRepository;
            this._productRepository = productRepository;
            this._subCategoryRepository = subCategoryRepository;
            this._productSubCategoryRepository = productSubCategoryRepository;
            this._ratePlusCommentRepository = ratePlusCommentRepository;
            this._userRepository = userRepository;
        }

        //
        // GET: /Products/


        public ViewResult Index()
        {
            return View(_productRepository.AllIncluding(product => product.Category, product => product.SubCategories));
        }

        //
        // GET: /Products/Details/5

        public ViewResult Details(int id)
        {
            return View(_productRepository.Find(id));
        }

        //
        // GET: /Products/ProductDetails/5

        public ViewResult ProductDetails(int id)
        {
            return View(_productRepository.Find(id));
        }
        //
        // GET: /Products/Create
		[Authorize(Roles="Admin, Editor")]
        public ActionResult Create()
        {
            //Create a list containing both selected and non-selected subcategories for the product
            List<SubCategory> subCategoriesInSystem = _subCategoryRepository.All.ToList();
            List<ItemCheckSelected> subCategoriesAvailable = new List<ItemCheckSelected>();

            foreach (SubCategory subCategory in subCategoriesInSystem)
            {
                subCategoriesAvailable.Add(new ItemCheckSelected()
                {
                    ItemId = subCategory.SubCategoryId,
                    Name = subCategory.Name,
                    Selected = false
                });
            }
            ViewBag.SubCategories = subCategoriesAvailable;
            ViewBag.PossibleCategories = _categoryRepository.All;
            return View();
        }

        //
        // POST: /Products/Create

        [HttpPost]
		[Authorize(Roles = "Admin, Editor")]
		public ActionResult Create(Product product, HttpPostedFileBase image, List<ItemCheckSelected> productSubCategory) // manually added HttpPostedFileBase image
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

                _productRepository.InsertOrUpdate(product);
                _productRepository.Save();

                //Add new connections
                foreach (ItemCheckSelected subCatSelected in productSubCategory.FindAll(c => c.Selected == true))
                {
                    ProductSubCategory subCatConnection = new ProductSubCategory()
                    {
                        ProductId = product.ProductId,
                        SubCategoryId = subCatSelected.ItemId
                    };
                    _productSubCategoryRepository.InsertOrUpdate(subCatConnection);
                }
                _productSubCategoryRepository.Save();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.SubCategories = productSubCategory;
                ViewBag.PossibleCategories = _categoryRepository.All;
                return View();
            }
        }

        //
        // GET: /Products/Edit/5
		[Authorize(Roles = "Admin, Editor")]
        public ActionResult Edit(int id)
        {
            Product product = _productRepository.Find(id);
            //Create a list containing both selected and non-selected subcategories for the product
            List<SubCategory> subCategoriesInSystem = _subCategoryRepository.All.ToList();
            List<SubCategory> productSubCategoriesSelected = product.SubCategories.Select(c => c.SubCategory).ToList();
            List<ItemCheckSelected> subCategoriesAvailable = new List<ItemCheckSelected>();

            foreach (SubCategory subCategory in subCategoriesInSystem)
            {
                subCategoriesAvailable.Add(new ItemCheckSelected()
                {
                    ItemId = subCategory.SubCategoryId,
                    Name = subCategory.Name,
                    Selected = (productSubCategoriesSelected.FirstOrDefault(c => c.SubCategoryId == subCategory.SubCategoryId) != null) ? true : false
                });
            }
            TempData.Remove("ImageData");
            TempData.Remove("ImageMimeType");
            TempData.Add("ImageData", product.ImageData);
            TempData.Add("ImageMimeType", product.ImageMimeType);
            ViewBag.SubCategories = subCategoriesAvailable;
            ViewBag.PossibleCategories = _categoryRepository.All;
            return View(_productRepository.Find(id));
        }

        //
        // POST: /Products/Edit/5
		[Authorize(Roles = "Admin, Editor")]
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image, List<ItemCheckSelected> productSubCategory) // manually added HttpPostedFileBase image
        {
            if (ModelState.IsValid)
            {
                //Remove all existing ProductSubcategory entries for this product
                List<ProductSubCategory> subCatConnections = _productSubCategoryRepository.All.ToList().FindAll(s => s.ProductId == product.ProductId).ToList();

                //productSubCategoryRepository.All.ToList().RemoveAll(s => s.ProductId == product.ProductId);

                foreach (ProductSubCategory subCatConnection in subCatConnections)
                {
                    //context.ProductSubCategories.Remove(SubCatConnection);
                    _productSubCategoryRepository.Delete(subCatConnection.ProductSubCategoryId);
                }
                //productSubCategoryRepository.Save();

                //Add new connections
                foreach (ItemCheckSelected subCatSelected in productSubCategory.FindAll(c => c.Selected == true))
                {
                    ProductSubCategory subCatConnection = new ProductSubCategory()
                    {
                        ProductId = product.ProductId,
                        SubCategoryId = subCatSelected.ItemId
                    };
                    _productSubCategoryRepository.InsertOrUpdate(subCatConnection);
                    //context.ProductSubCategories.Add(SubCatConnection);
                }
                _productSubCategoryRepository.Save();
                // manually added 
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);


                }
                else
                {
                    product.ImageMimeType = (string)TempData["ImageMimeType"];
                    product.ImageData = (byte[])TempData["ImageData"];
                    //image.InputStream.Read(product.ImageData, 0, image.ContentLength);


                }
                // manually added end
                //
                //ViewBag.product.ImageData = TempData["currentImage"];
                _productRepository.InsertOrUpdate(product);
                _productRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {

                ViewBag.SubCategories = productSubCategory;
                ViewBag.PossibleCategories = _categoryRepository.All;
                return View();
            }
        }

		[Authorize(Roles = "Admin, Editor")]
        public ActionResult Delete(int id)
        {
            return View(_productRepository.Find(id));
        }

        [HttpPost, ActionName("Delete")]
		[Authorize(Roles = "Admin, Editor")]
        public ActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id);
            _productRepository.Save();

            return RedirectToAction("Index");
        }

        public ViewResult List(string category, string subCategory = null, int page = 1)
        {
			IEnumerable<Product> products = _productRepository.AllIncluding(product => product.Category, product => product.SubCategories)
				.Where(c => c.Category.Name == category)
                .OrderBy(p => p.ProductId);

			if (subCategory != null)
			{
				products = _productRepository.AllIncluding(product => product.Category, product => product.SubCategories)
				.Where(c => c.Category.Name == category)
				.Where(c => c.SubCategories.FirstOrDefault(sc => sc.SubCategory.Name == subCategory).SubCategory.Name == subCategory)
				.OrderBy(p => p.ProductId);
			}

            ProductListView model = new ProductListView
            {
                Products = products,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _productRepository.All.Count()
                    //TotalItems = category == null ?
                    //    productRepository.All.Count() :
                    //    productRepository.All.Where(e => e.Categories == category).Count()
                },
                CurrentCategory = category
            };
			ViewBag.PageCategory = (string)category;
			ViewBag.PageSubcategory = (string)subCategory;
            return View(model);
        }

        // view product summary and rating statistic
        public ViewResult SummaryDetails(int id)
        {
            if (_ratePlusCommentRepository.All.ToList().FindAll(o => o.ProductId == id).Count() < 1)
            {
                ViewBag.ratingstatistics = "This product is not rated yet";
                ViewBag.ratecomments = "";
            }
            else
            {
                ViewBag.ratingstatistics = string.Format("{0:0.#}", _ratePlusCommentRepository.All.ToList().FindAll(o => o.ProductId == id).Average(p => p.Rate));
                ViewBag.ratecomments = _ratePlusCommentRepository.All.ToList().FindAll(o => o.ProductId == id);
                ViewBag.ratingUser = "test USER"; //userRepository.All.ToList().FindAll(o => o.UserId == ViewBag.ratecomments.UserId);
                //ViewBag.ratinguser = userRepository.All.ToList().FirstOrDefault(o => o.UserId == );
            }
            return View(_productRepository.Find(id));
        }


        public FileContentResult GetImage(int productid)
        {
            //Product prod = productRepository.All.FirstOrDefault(p => p.ProductId == id);
            Product prod = _productRepository.All.ToList().FirstOrDefault(p => p.ProductId == productid);
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
                _categoryRepository.Dispose();
                _productRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

