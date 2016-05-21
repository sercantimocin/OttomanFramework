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
    // Shippers
    public class Shipper
    {
        public int Id { get; set; } // Id (Primary key)
        public string CompanyName { get; set; } // CompanyName
        public string Phone { get; set; } // Phone

        // Reverse navigation
        public virtual ICollection<Order> Orders { get; set; } // Orders.FK_Orders_Shippers

        public Shipper()
        {
            Orders = new List<Order>();
        }
    }

}
