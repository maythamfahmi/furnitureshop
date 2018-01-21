using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class SubCategoryRepository : ISubCategoryRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<SubCategory> All
        {
            get { return _context.SubCategories; }
        }

        public IQueryable<SubCategory> AllIncluding(params Expression<Func<SubCategory, object>>[] includeProperties)
        {
            IQueryable<SubCategory> query = _context.SubCategories;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public SubCategory Find(int id)
        {
            return _context.SubCategories.Find(id);
        }

        public void InsertOrUpdate(SubCategory subcategory)
        {
            if (subcategory.SubCategoryId == default(int)) {
                // New entity
                _context.SubCategories.Add(subcategory);
            } else {
                // Existing entity
                _context.Entry(subcategory).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var subcategory = _context.SubCategories.Find(id);
            _context.SubCategories.Remove(subcategory);
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