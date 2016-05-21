namespace Ottoman.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    // Order Details
    internal class OrderDetailMap : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailMap(string schema = "dbo")
        {
            this.ToTable(schema + ".Order Details");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.OrderId).HasColumnName("OrderId").IsRequired();
            this.Property(x => x.ProductId).HasColumnName("ProductId").IsRequired();
            this.Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsRequired().HasPrecision(19,4);
            this.Property(x => x.Quantity).HasColumnName("Quantity").IsRequired();
            this.Property(x => x.Discount).HasColumnName("Discount").IsRequired();

            // Foreign keys
            this.HasRequired(a => a.Order).WithMany(b => b.OrderDetails).HasForeignKey(c => c.OrderId); // FK_Order_Details_Orders
            this.HasRequired(a => a.Product).WithMany(b => b.OrderDetails).HasForeignKey(c => c.ProductId); // FK_Order_Details_Products
        }
    }

}
