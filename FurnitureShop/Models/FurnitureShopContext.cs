using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FurnitureShop.Models
{
    public class FurnitureShopContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<FurnitureShop.Models.FurnitureShopContext>());

        public DbSet<FurnitureShop.Models.Address> Addresses { get; set; }

        public DbSet<FurnitureShop.Models.Category> Categories { get; set; }

        public DbSet<FurnitureShop.Models.Order> Orders { get; set; }

        public DbSet<FurnitureShop.Models.OrderDelivery> OrderDeliveries { get; set; }

        public DbSet<FurnitureShop.Models.OrderProduct> OrderProducts { get; set; }

        public DbSet<FurnitureShop.Models.Product> Products { get; set; }

        public DbSet<FurnitureShop.Models.ProductSubCategory> ProductSubCategories { get; set; }

        public DbSet<FurnitureShop.Models.RatePlusComment> RatePlusComments { get; set; }

        public DbSet<FurnitureShop.Models.SpecialOffer> SpecialOffers { get; set; }

        public DbSet<FurnitureShop.Models.SubCategory> SubCategories { get; set; }

        public DbSet<FurnitureShop.Models.User> Users { get; set; }

        public DbSet<FurnitureShop.Models.UserRole> UserRoles { get; set; }
    }
}