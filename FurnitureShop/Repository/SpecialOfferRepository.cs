using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class SpecialOfferRepository : ISpecialOfferRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<SpecialOffer> All
        {
            get { return _context.SpecialOffers; }
        }

        public IQueryable<SpecialOffer> AllIncluding(params Expression<Func<SpecialOffer, object>>[] includeProperties)
        {
            IQueryable<SpecialOffer> query = _context.SpecialOffers;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public SpecialOffer Find(int id)
        {
            return _context.SpecialOffers.Find(id);
        }

        public void InsertOrUpdate(SpecialOffer specialoffer)
        {
            if (specialoffer.SpecialOfferId == default(int)) {
                // New entity
                _context.SpecialOffers.Add(specialoffer);
            } else {
                // Existing entity
                _context.Entry(specialoffer).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var specialoffer = _context.SpecialOffers.Find(id);
            _context.SpecialOffers.Remove(specialoffer);
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

    public interface ISpecialOfferRepository : IDisposable
    {
        IQueryable<SpecialOffer> All { get; }
        IQueryable<SpecialOffer> AllIncluding(params Expression<Func<SpecialOffer, object>>[] includeProperties);
        SpecialOffer Find(int id);
        void InsertOrUpdate(SpecialOffer specialoffer);
        void Delete(int id);
        void Save();
    }
}