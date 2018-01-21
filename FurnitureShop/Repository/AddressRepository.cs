using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class AddressRepository : IAddressRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<Address> All
        {
            get { return _context.Addresses; }
        }

        public IQueryable<Address> AllIncluding(params Expression<Func<Address, object>>[] includeProperties)
        {
            IQueryable<Address> query = _context.Addresses;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Address Find(int id)
        {
            return _context.Addresses.Find(id);
        }

        public void InsertOrUpdate(Address address)
        {
            if (address.AddressId == default(int)) {
                // New entity
                _context.Addresses.Add(address);
            } else {
                // Existing entity
                _context.Entry(address).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var address = _context.Addresses.Find(id);
            _context.Addresses.Remove(address);
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

    public interface IAddressRepository : IDisposable
    {
        IQueryable<Address> All { get; }
        IQueryable<Address> AllIncluding(params Expression<Func<Address, object>>[] includeProperties);
        Address Find(int id);
        void InsertOrUpdate(Address address);
        void Delete(int id);
        void Save();
    }
}