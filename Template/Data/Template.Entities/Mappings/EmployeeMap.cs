namespace Ottoman.Entities.Mappings
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    // Employees
    internal class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap(string schema = "dbo")
        {
            this.ToTable(schema + ".Employees");
            this.HasKey(x => x.Id);

            this.Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(x => x.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(20);
            this.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(10);
            this.Property(x => x.Title).HasColumnName("Title").IsOptional().HasMaxLength(30);
            this.Property(x => x.TitleOfCourtesy).HasColumnName("TitleOfCourtesy").IsOptional().HasMaxLength(25);
            this.Property(x => x.BirthDate).HasColumnName("BirthDate").IsOptional();
            this.Property(x => x.HireDate).HasColumnName("HireDate").IsOptional();
            this.Property(x => x.Address).HasColumnName("Address").IsOptional().HasMaxLength(60);
            this.Property(x => x.City).HasColumnName("City").IsOptional().HasMaxLength(15);
            this.Property(x => x.Region).HasColumnName("Region").IsOptional().HasMaxLength(15);
            this.Property(x => x.PostalCode).HasColumnName("PostalCode").IsOptional().HasMaxLength(10);
            this.Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(15);
            this.Property(x => x.HomePhone).HasColumnName("HomePhone").IsOptional().HasMaxLength(24);
            this.Property(x => x.Extension).HasColumnName("Extension").IsOptional().HasMaxLength(4);
            this.Property(x => x.Photo).HasColumnName("Photo").IsOptional().HasMaxLength(2147483647);
            this.Property(x => x.Notes).HasColumnName("Notes").IsOptional().HasMaxLength(1073741823);
            this.Property(x => x.ReportsTo).HasColumnName("ReportsTo").IsOptional();
            this.Property(x => x.PhotoPath).HasColumnName("PhotoPath").IsOptional().HasMaxLength(255);

            // Foreign keys
            this.HasOptional(a => a.Employee_ReportsTo).WithMany(b => b.Employees).HasForeignKey(c => c.ReportsTo); // FK_Employees_Employees
            this.HasMany(t => t.Territories).WithMany(t => t.Employees).Map(m => 
            {
                m.ToTable("EmployeeTerritories", schema);
                m.MapLeftKey("EmployeeId");
                m.MapRightKey("TerritoryId");
            });
        }
    }

}
