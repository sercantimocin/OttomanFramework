namespace Demo.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    // Orders
    internal class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap(string schema = "dbo")
        {
            this.ToTable(schema + ".Orders");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.CustomerId).HasColumnName("CustomerId").IsOptional();
            this.Property(x => x.EmployeeId).HasColumnName("EmployeeId").IsOptional();
            this.Property(x => x.OrderDate).HasColumnName("OrderDate").IsOptional();
            this.Property(x => x.RequiredDate).HasColumnName("RequiredDate").IsOptional();
            this.Property(x => x.ShippedDate).HasColumnName("ShippedDate").IsOptional();
            this.Property(x => x.ShipVia).HasColumnName("ShipVia").IsOptional();
            this.Property(x => x.Freight).HasColumnName("Freight").IsOptional().HasPrecision(19,4);
            this.Property(x => x.ShipName).HasColumnName("ShipName").IsOptional().HasMaxLength(40);
            this.Property(x => x.ShipAddress).HasColumnName("ShipAddress").IsOptional().HasMaxLength(60);
            this.Property(x => x.ShipCity).HasColumnName("ShipCity").IsOptional().HasMaxLength(15);
            this.Property(x => x.ShipRegion).HasColumnName("ShipRegion").IsOptional().HasMaxLength(15);
            this.Property(x => x.ShipPostalCode).HasColumnName("ShipPostalCode").IsOptional().HasMaxLength(10);
            this.Property(x => x.ShipCountry).HasColumnName("ShipCountry").IsOptional().HasMaxLength(15);

            // Foreign keys
            this.HasOptional(a => a.Customer).WithMany(b => b.Orders).HasForeignKey(c => c.CustomerId); // FK_Orders_Customers
            this.HasOptional(a => a.Employee).WithMany(b => b.Orders).HasForeignKey(c => c.EmployeeId); // FK_Orders_Employees
            this.HasOptional(a => a.Shipper).WithMany(b => b.Orders).HasForeignKey(c => c.ShipVia); // FK_Orders_Shippers
        }
    }

}
