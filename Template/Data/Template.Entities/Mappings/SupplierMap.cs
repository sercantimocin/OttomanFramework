namespace Ottoman.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Template.Entities;

    // Suppliers
    internal class SupplierMap : EntityTypeConfiguration<Supplier>
    {
        public SupplierMap(string schema = "dbo")
        {
            this.ToTable(schema + ".Suppliers");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            this.Property(x => x.ContactName).HasColumnName("ContactName").IsOptional().HasMaxLength(30);
            this.Property(x => x.ContactTitle).HasColumnName("ContactTitle").IsOptional().HasMaxLength(30);
            this.Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            this.Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            this.Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            this.Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            this.Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            this.Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(24);
            this.Property(x => x.Fax).HasColumnName("Fax").IsOptional().HasMaxLength(24);
            this.Property(x => x.HomePage).HasColumnName("HomePage").IsOptional().HasMaxLength(1073741823);
        }
    }

}
