using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class UserRepository : IUserRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<User> All
        {
            get { return _context.Users; }
        }

        public IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> query = _context.Users;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public User Find(int id)
        {
            return _context.Users.Find(id);
        }

        public void InsertOrUpdate(User user)
        {
            if (user.UserId == default(int)) {
                // New entity
                _context.Users.Add(user);
            } else {
                // Existing entity
                _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
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

    public interface IUserRepository : IDisposable
    {
        IQueryable<User> All { get; }
        IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties);
        User Find(int id);
        void InsertOrUpdate(User user);
        void Delete(int id);
        void Save();
    }
}