using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class OrderRepository : IOrderRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<Order> All
        {
            get { return _context.Orders; }
        }

        public IQueryable<Order> AllIncluding(params Expression<Func<Order, object>>[] includeProperties)
        {
            IQueryable<Order> query = _context.Orders;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Order Find(int id)
        {
            return _context.Orders.Find(id);
        }

        public void InsertOrUpdate(Order order)
        {
            if (order.OrderId == default(int)) {
                // New entity
                _context.Orders.Add(order);
            } else {
                // Existing entity
                _context.Entry(order).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var order = _context.Orders.Find(id);
            _context.Orders.Remove(order);
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

    public interface IOrderRepository : IDisposable
    {
        IQueryable<Order> All { get; }
        IQueryable<Order> AllIncluding(params Expression<Func<Order, object>>[] includeProperties);
        Order Find(int id);
        void InsertOrUpdate(Order order);
        void Delete(int id);
        void Save();
    }
}