using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class OrderDeliveryRepository : IOrderDeliveryRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<OrderDelivery> All
        {
            get { return _context.OrderDeliveries; }
        }

        public IQueryable<OrderDelivery> AllIncluding(params Expression<Func<OrderDelivery, object>>[] includeProperties)
        {
            IQueryable<OrderDelivery> query = _context.OrderDeliveries;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderDelivery Find(int id)
        {
            return _context.OrderDeliveries.Find(id);
        }

        public void InsertOrUpdate(OrderDelivery orderdelivery)
        {
            if (orderdelivery.OrderDeliveryId == default(int)) {
                // New entity
                _context.OrderDeliveries.Add(orderdelivery);
            } else {
                // Existing entity
                _context.Entry(orderdelivery).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var orderdelivery = _context.OrderDeliveries.Find(id);
            _context.OrderDeliveries.Remove(orderdelivery);
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

    public interface IOrderDeliveryRepository : IDisposable
    {
        IQueryable<OrderDelivery> All { get; }
        IQueryable<OrderDelivery> AllIncluding(params Expression<Func<OrderDelivery, object>>[] includeProperties);
        OrderDelivery Find(int id);
        void InsertOrUpdate(OrderDelivery orderdelivery);
        void Delete(int id);
        void Save();
    }
}