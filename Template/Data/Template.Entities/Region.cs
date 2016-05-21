// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.6.1

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.ModelConfiguration;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace Ottoman.Entities
{
    // Region
    public class Region
    {
        public int Id { get; set; } // Id (Primary key)
        public string RegionDescription { get; set; } // RegionDescription

        // Reverse navigation
        public virtual ICollection<Territory> Territories { get; set; } // Territories.FK_Territories_Region

        public Region()
        {
            Territories = new List<Territory>();
        }
    }

}
