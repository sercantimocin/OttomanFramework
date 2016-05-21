namespace Ottoman.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    // Shippers
    internal class ShipperMap : EntityTypeConfiguration<Shipper>
    {
        public ShipperMap(string schema = "dbo")
        {
            this.ToTable(schema + ".Shippers");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.CompanyName).HasColumnName("CompanyName").IsRequired().HasMaxLength(40);
            this.Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(24);
        }
    }

}
