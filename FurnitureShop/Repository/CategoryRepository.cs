using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class CategoryRepository : ICategoryRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<Category> All
        {
            get { return _context.Categories; }
        }

        public IQueryable<Category> AllIncluding(params Expression<Func<Category, object>>[] includeProperties)
        {
            IQueryable<Category> query = _context.Categories;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Category Find(int id)
        {
            return _context.Categories.Find(id);
        }

        public void InsertOrUpdate(Category category)
        {
            if (category.CategoryId == default(int)) {
                // New entity
                _context.Categories.Add(category);
            } else {
                // Existing entity
                _context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
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

    public interface ICategoryRepository : IDisposable
    {
        IQueryable<Category> All { get; }
        IQueryable<Category> AllIncluding(params Expression<Func<Category, object>>[] includeProperties);
        Category Find(int id);
        void InsertOrUpdate(Category category);
        void Delete(int id);
        void Save();
    }
}