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
    public class SubCategoryRepository : ISubCategoryRepository
    {
        FurnitureShopContext context = new FurnitureShopContext();

        public IQueryable<SubCategory> All
        {
            get { return context.SubCategories; }
        }

        public IQueryable<SubCategory> AllIncluding(params Expression<Func<SubCategory, object>>[] includeProperties)
        {
            IQueryable<SubCategory> query = context.SubCategories;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public SubCategory Find(int id)
        {
            return context.SubCategories.Find(id);
        }

        public void InsertOrUpdate(SubCategory subcategory)
        {
            if (subcategory.SubCategoryId == default(int)) {
                // New entity
                context.SubCategories.Add(subcategory);
            } else {
                // Existing entity
                context.Entry(subcategory).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var subcategory = context.SubCategories.Find(id);
            context.SubCategories.Remove(subcategory);
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

    public interface ISubCategoryRepository : IDisposable
    {
        IQueryable<SubCategory> All { get; }
        IQueryable<SubCategory> AllIncluding(params Expression<Func<SubCategory, object>>[] includeProperties);
        SubCategory Find(int id);
        void InsertOrUpdate(SubCategory subcategory);
        void Delete(int id);
        void Save();
    }
}