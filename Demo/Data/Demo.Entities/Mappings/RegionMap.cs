namespace Demo.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using Demo.Entities;

    // Region
    internal class RegionMap : EntityTypeConfiguration<Region>
    {
        public RegionMap(string schema = "dbo")
        {
            this.ToTable(schema + ".Region");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.RegionDescription).HasColumnName("RegionDescription").IsRequired().IsFixedLength().HasMaxLength(50);
        }
    }

}
