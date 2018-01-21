using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class RatePlusCommentRepository : IRatePlusCommentRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<RatePlusComment> All
        {
            get { return _context.RatePlusComments; }
        }

        public IQueryable<RatePlusComment> AllIncluding(params Expression<Func<RatePlusComment, object>>[] includeProperties)
        {
            IQueryable<RatePlusComment> query = _context.RatePlusComments;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public RatePlusComment Find(int id)
        {
            return _context.RatePlusComments.Find(id);
        }

        public void InsertOrUpdate(RatePlusComment ratepluscomment)
        {
            if (ratepluscomment.RateId == default(int)) {
                // New entity
                _context.RatePlusComments.Add(ratepluscomment);
            } else {
                // Existing entity
                _context.Entry(ratepluscomment).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var ratepluscomment = _context.RatePlusComments.Find(id);
            _context.RatePlusComments.Remove(ratepluscomment);
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