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
    public class RatePlusCommentRepository : IRatePlusCommentRepository
    {
        FurnitureShopContext context = new FurnitureShopContext();

        public IQueryable<RatePlusComment> All
        {
            get { return context.RatePlusComments; }
        }

        public IQueryable<RatePlusComment> AllIncluding(params Expression<Func<RatePlusComment, object>>[] includeProperties)
        {
            IQueryable<RatePlusComment> query = context.RatePlusComments;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public RatePlusComment Find(int id)
        {
            return context.RatePlusComments.Find(id);
        }

        public void InsertOrUpdate(RatePlusComment ratepluscomment)
        {
            if (ratepluscomment.RateId == default(int)) {
                // New entity
                context.RatePlusComments.Add(ratepluscomment);
            } else {
                // Existing entity
                context.Entry(ratepluscomment).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var ratepluscomment = context.RatePlusComments.Find(id);
            context.RatePlusComments.Remove(ratepluscomment);
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

    public interface IRatePlusCommentRepository : IDisposable
    {
        IQueryable<RatePlusComment> All { get; }
        IQueryable<RatePlusComment> AllIncluding(params Expression<Func<RatePlusComment, object>>[] includeProperties);
        RatePlusComment Find(int id);
        void InsertOrUpdate(RatePlusComment ratepluscomment);
        void Delete(int id);
        void Save();
    }
}