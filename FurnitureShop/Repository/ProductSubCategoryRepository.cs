using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class ProductSubCategoryRepository : IProductSubCategoryRepository
    {
        FurnitureShopContext context = new FurnitureShopContext();

        public IQueryable<ProductSubCategory> All
        {
            get { return context.ProductSubCategories; }
        }

        public IQueryable<ProductSubCategory> AllIncluding(params Expression<Func<ProductSubCategory, object>>[] includeProperties)
        {
            IQueryable<ProductSubCategory> query = context.ProductSubCategories;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ProductSubCategory Find(int id)
        {
            return context.ProductSubCategories.Find(id);
        }

        public void InsertOrUpdate(ProductSubCategory productsubcategory)
        {
            if (productsubcategory.ProductSubCategoryId == default(int)) {
                // New entity
                context.ProductSubCategories.Add(productsubcategory);
            } else {
                // Existing entity
                context.Entry(productsubcategory).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var productsubcategory = context.ProductSubCategories.Find(id);
            context.ProductSubCategories.Remove(productsubcategory);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
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