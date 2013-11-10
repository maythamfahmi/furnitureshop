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
    public class OrderDeliveryRepository : IOrderDeliveryRepository
    {
        FurnitureShopContext context = new FurnitureShopContext();

        public IQueryable<OrderDelivery> All
        {
            get { return context.OrderDeliveries; }
        }

        public IQueryable<OrderDelivery> AllIncluding(params Expression<Func<OrderDelivery, object>>[] includeProperties)
        {
            IQueryable<OrderDelivery> query = context.OrderDeliveries;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderDelivery Find(int id)
        {
            return context.OrderDeliveries.Find(id);
        }

        public void InsertOrUpdate(OrderDelivery orderdelivery)
        {
            if (orderdelivery.OrderDeliveryId == default(int)) {
                // New entity
                context.OrderDeliveries.Add(orderdelivery);
            } else {
                // Existing entity
                context.Entry(orderdelivery).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var orderdelivery = context.OrderDeliveries.Find(id);
            context.OrderDeliveries.Remove(orderdelivery);
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