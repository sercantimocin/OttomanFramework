// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.6.1

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.ModelConfiguration;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace Ottoman.Entities
{
    public class OttomanDbContext : DbContext, IOttomanDbContext
    {
        public IDbSet<Category> Categories { get; set; } // Categories
        public IDbSet<Customer> Customers { get; set; } // Customers
        public IDbSet<Employee> Employees { get; set; } // Employees
        public IDbSet<Order> Orders { get; set; } // Orders
        public IDbSet<OrderDetail> OrderDetails { get; set; } // Order Details
        public IDbSet<Product> Products { get; set; } // Products
        public IDbSet<Region> Regions { get; set; } // Region
        public IDbSet<Shipper> Shippers { get; set; } // Shippers
        public IDbSet<Supplier> Suppliers { get; set; } // Suppliers
        public IDbSet<Territory> Territories { get; set; } // Territories

        static OttomanDbContext()
        {
            Database.SetInitializer<OttomanDbContext>(null);
        }

        public OttomanDbContext()
            : base("Name=conStr")
        {
        }

        public OttomanDbContext(string connectionString) : base(connectionString)
        {
        }

        public OttomanDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new RegionMap());
            modelBuilder.Configurations.Add(new ShipperMap());
            modelBuilder.Configurations.Add(new SupplierMap());
            modelBuilder.Configurations.Add(new TerritoryMap());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new CategoryMap(schema));
            modelBuilder.Configurations.Add(new CustomerMap(schema));
            modelBuilder.Configurations.Add(new EmployeeMap(schema));
            modelBuilder.Configurations.Add(new OrderMap(schema));
            modelBuilder.Configurations.Add(new OrderDetailMap(schema));
            modelBuilder.Configurations.Add(new ProductMap(schema));
            modelBuilder.Configurations.Add(new RegionMap(schema));
            modelBuilder.Configurations.Add(new ShipperMap(schema));
            modelBuilder.Configurations.Add(new SupplierMap(schema));
            modelBuilder.Configurations.Add(new TerritoryMap(schema));
            return modelBuilder;
        }
        
        // Stored Procedures
    }
}
