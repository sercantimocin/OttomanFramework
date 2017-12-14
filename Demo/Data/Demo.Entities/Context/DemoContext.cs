namespace Demo.Entities.Context
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Mappings;

    using Repository.Pattern.Ef6;

    public class DemoContext : DataContext
    {
        static DemoContext()
        {
            Database.SetInitializer<DemoContext>(null);
        }

        public DemoContext() : base("Name=conStr")
        {
        }

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

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
