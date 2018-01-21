using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class ProductSubCategoryRepository : IProductSubCategoryRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<ProductSubCategory> All
        {
            get { return _context.ProductSubCategories; }
        }

        public IQueryable<ProductSubCategory> AllIncluding(params Expression<Func<ProductSubCategory, object>>[] includeProperties)
        {
            IQueryable<ProductSubCategory> query = _context.ProductSubCategories;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ProductSubCategory Find(int id)
        {
            return _context.ProductSubCategories.Find(id);
        }

        public void InsertOrUpdate(ProductSubCategory productsubcategory)
        {
            if (productsubcategory.ProductSubCategoryId == default(int)) {
                // New entity
                _context.ProductSubCategories.Add(productsubcategory);
            } else {
                // Existing entity
                _context.Entry(productsubcategory).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var productsubcategory = _context.ProductSubCategories.Find(id);
            _context.ProductSubCategories.Remove(productsubcategory);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose() 
        {
            _context.Dispose();
        }
    }

    public interface IProductSubCategoryRepository : IDisposable
    {
        IQueryable<ProductSubCategory> All { get; }
        IQueryable<ProductSubCategory> AllIncluding(params Expression<Func<ProductSubCategory, object>>[] includeProperties);
        ProductSubCategory Find(int id);
        void InsertOrUpdate(ProductSubCategory productsubcategory);
        void Delete(int id);
        void Save();
    }
}