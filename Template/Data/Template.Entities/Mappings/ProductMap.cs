namespace Ottoman.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Template.Entities;

    // Products
    internal class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap(string schema = "dbo")
        {
            this.ToTable(schema + ".Products");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(40);
            this.Property(x => x.SupplierId).HasColumnName("SupplierId").IsOptional();
            this.Property(x => x.CategoryId).HasColumnName("CategoryId").IsOptional();
            this.Property(x => x.QuantityPerUnit).HasColumnName("QuantityPerUnit").IsOptional().HasMaxLength(20);
            this.Property(x => x.UnitPrice).HasColumnName("UnitPrice").IsOptional().HasPrecision(19,4);
            this.Property(x => x.UnitsInStock).HasColumnName("UnitsInStock").IsOptional();
            this.Property(x => x.UnitsOnOrder).HasColumnName("UnitsOnOrder").IsOptional();
            this.Property(x => x.ReorderLevel).HasColumnName("ReorderLevel").IsOptional();
            this.Property(x => x.Discontinued).HasColumnName("Discontinued").IsRequired();

            // Foreign keys
            this.HasOptional(a => a.Supplier).WithMany(b => b.Products).HasForeignKey(c => c.SupplierId); // FK_Products_Suppliers
            this.HasOptional(a => a.Category).WithMany(b => b.Products).HasForeignKey(c => c.CategoryId); // FK_Products_Categories
        }
    }

}
