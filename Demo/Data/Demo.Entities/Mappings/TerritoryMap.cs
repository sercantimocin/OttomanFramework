namespace Demo.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    // Territories
    internal class TerritoryMap : EntityTypeConfiguration<Territory>
    {
        public TerritoryMap(string schema = "dbo")
        {
            this.ToTable(schema + ".Territories");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id).HasColumnName("Id").IsRequired().HasMaxLength(20).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(x => x.TerritoryDescription).HasColumnName("TerritoryDescription").IsRequired().IsFixedLength().HasMaxLength(50);
            this.Property(x => x.RegionId).HasColumnName("RegionId").IsRequired();

            // Foreign keys
            this.HasRequired(a => a.Region).WithMany(b => b.Territories).HasForeignKey(c => c.RegionId); // FK_Territories_Region
        }
    }

}
