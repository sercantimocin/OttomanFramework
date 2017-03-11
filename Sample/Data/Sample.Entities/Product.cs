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

    // Products
    public class Product
    {
        public int Id { get; set; } // Id (Primary key)
        public string ProductName { get; set; } // ProductName
        public int? SupplierId { get; set; } // SupplierId
        public int? CategoryId { get; set; } // CategoryId
        public string QuantityPerUnit { get; set; } // QuantityPerUnit
        public decimal? UnitPrice { get; set; } // UnitPrice
        public short? UnitsInStock { get; set; } // UnitsInStock
        public short? UnitsOnOrder { get; set; } // UnitsOnOrder
        public short? ReorderLevel { get; set; } // ReorderLevel
        public bool Discontinued { get; set; } // Discontinued

        // Reverse navigation
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } // Order Details.FK_Order_Details_Products

        // Foreign keys
        public virtual Category Category { get; set; } // FK_Products_Categories
        public virtual Supplier Supplier { get; set; } // FK_Products_Suppliers

        public Product()
        {
            this.UnitPrice = 0m;
            this.UnitsInStock = 0;
            this.UnitsOnOrder = 0;
            this.ReorderLevel = 0;
            this.Discontinued = false;
            this.OrderDetails = new List<OrderDetail>();
        }
    }

}
