using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FurnitureShop.Models;

namespace FurnitureShop.Repository
{ 
    public class UserRoleRepository : IUserRoleRepository
    {
        readonly FurnitureShopContext _context = new FurnitureShopContext();

        public IQueryable<UserRole> All
        {
            get { return _context.UserRoles; }
        }

        public IQueryable<UserRole> AllIncluding(params Expression<Func<UserRole, object>>[] includeProperties)
        {
            IQueryable<UserRole> query = _context.UserRoles;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public UserRole Find(int id)
        {
            return _context.UserRoles.Find(id);
        }

        public void InsertOrUpdate(UserRole userrole)
        {
            if (userrole.UserRoleId == default(int)) {
                // New entity
                _context.UserRoles.Add(userrole);
            } else {
                // Existing entity
                _context.Entry(userrole).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var userrole = _context.UserRoles.Find(id);
            _context.UserRoles.Remove(userrole);
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

    public interface IUserRoleRepository : IDisposable
    {
        IQueryable<UserRole> All { get; }
        IQueryable<UserRole> AllIncluding(params Expression<Func<UserRole, object>>[] includeProperties);
        UserRole Find(int id);
        void InsertOrUpdate(UserRole userrole);
        void Delete(int id);
        void Save();
    }
}