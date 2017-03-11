// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.6.1

//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace Sample.Entities
{
    using System.Collections.Generic;

    // Territories
    public class Territory
    {
        public string Id { get; set; } // Id (Primary key)
        public string TerritoryDescription { get; set; } // TerritoryDescription
        public int RegionId { get; set; } // RegionId

        // Reverse navigation
        public virtual ICollection<Employee> Employees { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Region Region { get; set; } // FK_Territories_Region

        public Territory()
        {
            this.Employees = new List<Employee>();
        }
    }

}
