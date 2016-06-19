

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Configuration file:     "SolutionFiles\HelperTools\PocoGenarator\App.config"
//     Connection String Name: "conStr"
//     Connection String:      "Data Source=LoveIsDev\MSS;Initial Catalog=Northwind;User ID=sa;Password=st89oss06bm08"

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
    // ************************************************************************
    // Unit of work
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

    // ************************************************************************
    // Database context
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

    // ************************************************************************
    // POCO classes

    // Categories
    public class Category
    {
        public int Id { get; set; } // Id (Primary key)
        public string CategoryName { get; set; } // CategoryName
        public string Description { get; set; } // Description
        public byte[] Picture { get; set; } // Picture

        // Reverse navigation
        public virtual ICollection<Product> Products { get; set; } // Products.FK_Products_Categories

        public Category()
        {
            Products = new List<Product>();
        }
    }

    // Customers
    public class Customer
    {
        public int Id { get; set; } // Id (Primary key)
        public string CompanyName { get; set; } // CompanyName
        public string ContactName { get; set; } // ContactName
        public string ContactTitle { get; set; } // ContactTitle
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string Phone { get; set; } // Phone
        public string Fax { get; set; } // Fax

        // Reverse navigation
        public virtual ICollection<Order> Orders { get; set; } // Orders.FK_Orders_Customers

        public Customer()
        {
            Orders = new List<Order>();
        }
    }

    // Employees
    public class Employee
    {
        public int Id { get; set; } // Id (Primary key)
        public string LastName { get; set; } // LastName
        public string FirstName { get; set; } // FirstName
        public string Title { get; set; } // Title
        public string TitleOfCourtesy { get; set; } // TitleOfCourtesy
        public DateTime? BirthDate { get; set; } // BirthDate
        public DateTime? HireDate { get; set; } // HireDate
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string HomePhone { get; set; } // HomePhone
        public string Extension { get; set; } // Extension
        public byte[] Photo { get; set; } // Photo
        public string Notes { get; set; } // Notes
        public int? ReportsTo { get; set; } // ReportsTo
        public string PhotoPath { get; set; } // PhotoPath

        // Reverse navigation
        public virtual ICollection<Employee> Employees { get; set; } // Employees.FK_Employees_Employees
        public virtual ICollection<Order> Orders { get; set; } // Orders.FK_Orders_Employees
        public virtual ICollection<Territory> Territories { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Employee Employee_ReportsTo { get; set; } // FK_Employees_Employees

        public Employee()
        {
            Employees = new List<Employee>();
            Orders = new List<Order>();
            Territories = new List<Territory>();
        }
    }

    // Orders
    public class Order
    {
        public int Id { get; set; } // Id (Primary key)
        public int? CustomerId { get; set; } // CustomerId
        public int? EmployeeId { get; set; } // EmployeeId
        public DateTime? OrderDate { get; set; } // OrderDate
        public DateTime? RequiredDate { get; set; } // RequiredDate
        public DateTime? ShippedDate { get; set; } // ShippedDate
        public int? ShipVia { get; set; } // ShipVia
        public decimal? Freight { get; set; } // Freight
        public string ShipName { get; set; } // ShipName
        public string ShipAddress { get; set; } // ShipAddress
        public string ShipCity { get; set; } // ShipCity
        public string ShipRegion { get; set; } // ShipRegion
        public string ShipPostalCode { get; set; } // ShipPostalCode
        public string ShipCountry { get; set; } // ShipCountry

        // Reverse navigation
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } // Order Details.FK_Order_Details_Orders

        // Foreign keys
        public virtual Customer Customer { get; set; } // FK_Orders_Customers
        public virtual Employee Employee { get; set; } // FK_Orders_Employees
        public virtual Shipper Shipper { get; set; } // FK_Orders_Shippers

        public Order()
        {
            Freight = 0m;
            OrderDetails = new List<OrderDetail>();
        }
    }

    // Order Details
    public class OrderDetail
    {
        public int Id { get; set; } // Id (Primary key)
        public int OrderId { get; set; } // OrderId
        public int ProductId { get; set; } // ProductId
        public decimal UnitPrice { get; set; } // UnitPrice
        public short Quantity { get; set; } // Quantity
        public float Discount { get; set; } // Discount

        // Foreign keys
        public virtual Order Order { get; set; } // FK_Order_Details_Orders
        public virtual Product Product { get; set; } // FK_Order_Details_Products

        public OrderDetail()
        {
            UnitPrice = 0m;
            Quantity = 1;
            Discount = 0;
        }
    }

    // Products
    public class Product
    {
        public int Id { get; set; } // Id (Primary key)
        public string ProductName { get; set; } // ProductName
        public int? SupplierId { get; set; } // SupplierId
        public int? CategoryId { get; set; } // CategoryId
        public string QuantityPerUnit { get; set; } // QuantityPerUnit
        public decimal? UnitPrice { get; set; } // UnitPrice
        public short? UnitsInStock { get; set; } // UnitsInStock
        public short? UnitsOnOrder { get; set; } // UnitsOnOrder
        public short? ReorderLevel { get; set; } // ReorderLevel
        public bool Discontinued { get; set; } // Discontinued

        // Reverse navigation
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } // Order Details.FK_Order_Details_Products

        // Foreign keys
        public virtual Category Category { get; set; } // FK_Products_Categories
        public virtual Supplier Supplier { get; set; } // FK_Products_Suppliers

        public Product()
        {
            UnitPrice = 0m;
            UnitsInStock = 0;
            UnitsOnOrder = 0;
            ReorderLevel = 0;
            Discontinued = false;
            OrderDetails = new List<OrderDetail>();
        }
    }

    // Region
    public class Region
    {
        public int Id { get; set; } // Id (Primary key)
        public string RegionDescription { get; set; } // RegionDescription

        // Reverse navigation
        public virtual ICollection<Territory> Territories { get; set; } // Territories.FK_Territories_Region

        public Region()
        {
            Territories = new List<Territory>();
        }
    }

    // Shippers
    public class Shipper
    {
        public int Id { get; set; } // Id (Primary key)
        public string CompanyName { get; set; } // CompanyName
        public string Phone { get; set; } // Phone

        // Reverse navigation
        public virtual ICollection<Order> Orders { get; set; } // Orders.FK_Orders_Shippers

        public Shipper()
        {
            Orders = new List<Order>();
        }
    }

    // Suppliers
    public class Supplier
    {
        public int Id { get; set; } // Id (Primary key)
        public string CompanyName { get; set; } // CompanyName
        public string ContactName { get; set; } // ContactName
        public string ContactTitle { get; set; } // ContactTitle
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string Phone { get; set; } // Phone
        public string Fax { get; set; } // Fax
        public string HomePage { get; set; } // HomePage

        // Reverse navigation
        public virtual ICollection<Product> Products { get; set; } // Products.FK_Products_Suppliers

        public Supplier()
        {
            Products = new List<Product>();
        }
    }

    // Territories
    public class Territory
    {
        public string Id { get; set; } // Id (Primary key)
        public string TerritoryDescription { get; set; } // TerritoryDescription
        public int RegionId { get; set; } // RegionId

        // Reverse navigation
        public virtual ICollection<Employee> Employees { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Region Region { get; set; } // FK_Territories_Region

        public Territory()
        {
            Employees = new List<Employee>();
        }
    }


    // ************************************************************************
    // POCO Configuration

    // Categories
    internal class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap(string schema = "dbo")
        {
            ToTable(schema + ".Categories");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasMaxLength(15);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(1073741823);
            Property(x => x.Picture).HasColumnName("Picture").IsOptional().HasMaxLength(2147483647);
        }
    }

    // Customers
    internal class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap(string schema = "dbo")
        {
            ToTable(schema + ".Customers");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.ContactName).HasColumnName("ContactName").IsOptional().HasMaxLength(30);
            Property(x => x.ContactTitle).HasColumnName("ContactTitle").IsOptional().HasMaxLength(30);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(24);
            Property(x => x.Fax).HasColumnName("Fax").IsOptional().HasMaxLength(24);
        }
    }

    // Employees
    internal class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap(string schema = "dbo")
        {
            ToTable(schema + ".Employees");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(20);
            Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(10);
            Property(x => x.Title).HasColumnName("Title").IsOptional().HasMaxLength(30);
            Property(x => x.TitleOfCourtesy).HasColumnName("TitleOfCourtesy").IsOptional().HasMaxLength(25);
            Property(x => x.BirthDate).HasColumnName("BirthDate").IsOptional();
            Property(x => x.HireDate).HasColumnName("HireDate").IsOptional();
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            Property(x => x.HomePhone).HasColumnName("HomePhone").IsOptional().HasMaxLength(24);
            Property(x => x.Extension).HasColumnName("Extension").IsOptional().HasMaxLength(4);
            Property(x => x.Photo).HasColumnName("Photo").IsOptional().HasMaxLength(2147483647);
            Property(x => x.Notes).HasColumnName("Notes").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ReportsTo).HasColumnName("ReportsTo").IsOptional();
            Property(x => x.PhotoPath).HasColumnName("PhotoPath").IsOptional().HasMaxLength(255);

            // Foreign keys
            HasOptional(a => a.Employee_ReportsTo).WithMany(b => b.Employees).HasForeignKey(c => c.ReportsTo); // FK_Employees_Employees
            HasMany(t => t.Territories).WithMany(t => t.Employees).Map(m => 
            {
                m.ToTable("EmployeeTerritories", schema);
                m.MapLeftKey("EmployeeId");
                m.MapRightKey("TerritoryId");
            });
        }
    }

    // Orders
    internal class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap(string schema = "dbo")
        {
            ToTable(schema + ".Orders");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CustomerId).HasColumnName("CustomerId").IsOptional();
            Property(x => x.EmployeeId).HasColumnName("EmployeeId").IsOptional();
            Property(x => x.OrderDate).HasColumnName("OrderDate").IsOptional();
            Property(x => x.RequiredDate).HasColumnName("RequiredDate").IsOptional();
            Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional();
            Property(x => x.ShipVia).HasColumnName("ShipVia").IsOptional();
            Property(x => x.Freight).HasColumnName("Freight").IsOptional().HasPrecision(19,4);
            Property(x => x.ShipName).HasColumnName("ShipName").IsOptional().HasMaxLength(40);
            Property(x => x.ShipAddress).HasColumnName("ShipAddress").IsOptional().HasMaxLength(60);
            Property(x => x.ShipCity).HasColumnName("ShipCity").IsOptional().HasMaxLength(15);
            Property(x => x.ShipRegion).HasColumnName("ShipRegion").IsOptional().HasMaxLength(15);
            Property(x => x.ShipPostalCode).HasColumnName("ShipPostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.ShipCountry).HasColumnName("ShipCountry").IsOptional().HasMaxLength(15);

            // Foreign keys
            HasOptional(a => a.Customer).WithMany(b => b.Orders).HasForeignKey(c => c.CustomerId); // FK_Orders_Customers
            HasOptional(a => a.Employee).WithMany(b => b.Orders).HasForeignKey(c => c.EmployeeId); // FK_Orders_Employees
            HasOptional(a => a.Shipper).WithMany(b => b.Orders).HasForeignKey(c => c.ShipVia); // FK_Orders_Shippers
        }
    }

    // Order Details
    internal class OrderDetailMap : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailMap(string schema = "dbo")
        {
            ToTable(schema + ".Order Details");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OrderId).HasColumnName("OrderId").IsRequired();
            Property(x => x.ProductId).HasColumnName("ProductId").IsRequired();
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsRequired().HasPrecision(19,4);
            Property(x => x.Quantity).HasColumnName("Quantity").IsRequired();
            Property(x => x.Discount).HasColumnName("Discount").IsRequired();

            // Foreign keys
            HasRequired(a => a.Order).WithMany(b => b.OrderDetails).HasForeignKey(c => c.OrderId); // FK_Order_Details_Orders
            HasRequired(a => a.Product).WithMany(b => b.OrderDetails).HasForeignKey(c => c.ProductId); // FK_Order_Details_Products
        }
    }

    // Products
    internal class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap(string schema = "dbo")
        {
            ToTable(schema + ".Products");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
            Property(x => x.SupplierId).HasColumnName("SupplierId").IsOptional();
            Property(x => x.CategoryId).HasColumnName("CategoryId").IsOptional();
            Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit").IsOptional().HasMaxLength(20);
            Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsOptional().HasPrecision(19,4);
            Property(x => x.UnitsInStock).HasColumnName("UnitsInStock").IsOptional();
            Property(x => x.UnitsOnOrder).HasColumnName("UnitsOnOrder").IsOptional();
            Property(x => x.ReorderLevel).HasColumnName("ReorderLevel").IsOptional();
            Property(x => x.Discontinued).HasColumnName("Discontinued").IsRequired();

            // Foreign keys
            HasOptional(a => a.Supplier).WithMany(b => b.Products).HasForeignKey(c => c.SupplierId); // FK_Products_Suppliers
            HasOptional(a => a.Category).WithMany(b => b.Products).HasForeignKey(c => c.CategoryId); // FK_Products_Categories
        }
    }

    // Region
    internal class RegionMap : EntityTypeConfiguration<Region>
    {
        public RegionMap(string schema = "dbo")
        {
            ToTable(schema + ".Region");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.RegionDescription).HasColumnName("RegionDescription").IsRequired().IsFixedLength().HasMaxLength(50);
        }
    }

    // Shippers
    internal class ShipperMap : EntityTypeConfiguration<Shipper>
    {
        public ShipperMap(string schema = "dbo")
        {
            ToTable(schema + ".Shippers");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(24);
        }
    }

    // Suppliers
    internal class SupplierMap : EntityTypeConfiguration<Supplier>
    {
        public SupplierMap(string schema = "dbo")
        {
            ToTable(schema + ".Suppliers");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            Property(x => x.ContactName).HasColumnName("ContactName").IsOptional().HasMaxLength(30);
            Property(x => x.ContactTitle).HasColumnName("ContactTitle").IsOptional().HasMaxLength(30);
            Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(24);
            Property(x => x.Fax).HasColumnName("Fax").IsOptional().HasMaxLength(24);
            Property(x => x.HomePage).HasColumnName("HomePage").IsOptional().HasMaxLength(1073741823);
        }
    }

    // Territories
    internal class TerritoryMap : EntityTypeConfiguration<Territory>
    {
        public TerritoryMap(string schema = "dbo")
        {
            ToTable(schema + ".Territories");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasMaxLength(20).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TerritoryDescription).HasColumnName("TerritoryDescription").IsRequired().IsFixedLength().HasMaxLength(50);
            Property(x => x.RegionId).HasColumnName("RegionId").IsRequired();

            // Foreign keys
            HasRequired(a => a.Region).WithMany(b => b.Territories).HasForeignKey(c => c.RegionId); // FK_Territories_Region
        }
    }


    // ************************************************************************
    // Stored procedure return models

}
