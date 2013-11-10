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
    public class OrderProductRepository : IOrderProductRepository
    {
        FurnitureShopContext context = new FurnitureShopContext();

        public IQueryable<OrderProduct> All
        {
            get { return context.OrderProducts; }
        }

        public IQueryable<OrderProduct> AllIncluding(params Expression<Func<OrderProduct, object>>[] includeProperties)
        {
            IQueryable<OrderProduct> query = context.OrderProducts;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrderProduct Find(int id)
        {
            return context.OrderProducts.Find(id);
        }

        public void InsertOrUpdate(OrderProduct orderproduct)
        {
            if (orderproduct.OrderProdcutId == default(int)) {
                // New entity
                context.OrderProducts.Add(orderproduct);
            } else {
                // Existing entity
                context.Entry(orderproduct).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var orderproduct = context.OrderProducts.Find(id);
            context.OrderProducts.Remove(orderproduct);
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