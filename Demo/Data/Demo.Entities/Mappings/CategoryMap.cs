namespace Demo.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    // Categories
    internal class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap(string schema = "dbo")
        {
            this.ToTable(schema + ".Categories");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired().HasMaxLength(15);
            this.Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(1073741823);
            this.Property(x => x.Picture).HasColumnName("Picture").IsOptional().HasMaxLength(2147483647);
        }
    }

}
