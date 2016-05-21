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
    public interface IOttomanDbContext : IDisposable
    {
        IDbSet<Category> Categories { get; set; } // Categories
        IDbSet<Customer> Customers { get; set; } // Customers
        IDbSet<Employee> Employees { get; set; } // Employees
        IDbSet<Order> Orders { get; set; } // Orders
        IDbSet<OrderDetail> OrderDetails { get; set; } // Order Details
        IDbSet<Product> Products { get; set; } // Products
        IDbSet<Region> Regions { get; set; } // Region
        IDbSet<Shipper> Shippers { get; set; } // Shippers
        IDbSet<Supplier> Suppliers { get; set; } // Suppliers
        IDbSet<Territory> Territories { get; set; } // Territories

        int SaveChanges();
        
        // Stored Procedures
    }

}
