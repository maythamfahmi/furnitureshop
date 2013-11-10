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
    public class UserRoleRepository : IUserRoleRepository
    {
        FurnitureShopContext context = new FurnitureShopContext();

        public IQueryable<UserRole> All
        {
            get { return context.UserRoles; }
        }

        public IQueryable<UserRole> AllIncluding(params Expression<Func<UserRole, object>>[] includeProperties)
        {
            IQueryable<UserRole> query = context.UserRoles;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public UserRole Find(int id)
        {
            return context.UserRoles.Find(id);
        }

        public void InsertOrUpdate(UserRole userrole)
        {
            if (userrole.UserRoleId == default(int)) {
                // New entity
                context.UserRoles.Add(userrole);
            } else {
                // Existing entity
                context.Entry(userrole).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var userrole = context.UserRoles.Find(id);
            context.UserRoles.Remove(userrole);
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