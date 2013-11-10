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
    public class SpecialOfferRepository : ISpecialOfferRepository
    {
        FurnitureShopContext context = new FurnitureShopContext();

        public IQueryable<SpecialOffer> All
        {
            get { return context.SpecialOffers; }
        }

        public IQueryable<SpecialOffer> AllIncluding(params Expression<Func<SpecialOffer, object>>[] includeProperties)
        {
            IQueryable<SpecialOffer> query = context.SpecialOffers;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public SpecialOffer Find(int id)
        {
            return context.SpecialOffers.Find(id);
        }

        public void InsertOrUpdate(SpecialOffer specialoffer)
        {
            if (specialoffer.SpecialOfferId == default(int)) {
                // New entity
                context.SpecialOffers.Add(specialoffer);
            } else {
                // Existing entity
                context.Entry(specialoffer).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var specialoffer = context.SpecialOffers.Find(id);
            context.SpecialOffers.Remove(specialoffer);
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