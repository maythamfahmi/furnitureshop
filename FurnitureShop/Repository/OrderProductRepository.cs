using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class OrderProductRepository : IOrderProductRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<OrderProduct> All
        {
            get { return _context.OrderProducts; }
        }

        public IQueryable<OrderProduct> AllIncluding(params Expression<Func<OrderProduct, object>>[] includeProperties)
        {
            IQueryable<OrderProduct> query = _context.OrderProducts;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderProduct Find(int id)
        {
            return _context.OrderProducts.Find(id);
        }

        public void InsertOrUpdate(OrderProduct orderproduct)
        {
            if (orderproduct.OrderProdcutId == default(int)) {
                // New entity
                _context.OrderProducts.Add(orderproduct);
            } else {
                // Existing entity
                _context.Entry(orderproduct).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var orderproduct = _context.OrderProducts.Find(id);
            _context.OrderProducts.Remove(orderproduct);
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

    public interface IOrderProductRepository : IDisposable
    {
        IQueryable<OrderProduct> All { get; }
        IQueryable<OrderProduct> AllIncluding(params Expression<Func<OrderProduct, object>>[] includeProperties);
        OrderProduct Find(int id);
        void InsertOrUpdate(OrderProduct orderproduct);
        void Delete(int id);
        void Save();
    }
}