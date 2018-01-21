using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class ProductRepository : IProductRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<Product> All
        {
            get { return _context.Products; }
        }

        public IQueryable<Product> AllIncluding(params Expression<Func<Product, object>>[] includeProperties)
        {
            IQueryable<Product> query = _context.Products;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Product Find(int id)
        {
            return _context.Products.Find(id);
        }

        public void InsertOrUpdate(Product product)
        {
            if (product.ProductId == default(int)) {
                // New entity
                _context.Products.Add(product);
            } else {
                // Existing entity
                _context.Entry(product).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
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

    public interface IProductRepository : IDisposable
    {
        IQueryable<Product> All { get; }
        IQueryable<Product> AllIncluding(params Expression<Func<Product, object>>[] includeProperties);
        Product Find(int id);
        void InsertOrUpdate(Product product);
        void Delete(int id);
        void Save();
    }
}