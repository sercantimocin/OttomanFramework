using System;
using System.Collections.Generic;

namespace Ottoman.Entities
{
    using Template.Entities;

    // Orders
    public class Order
    {
        public int Id { get; set; } // Id (Primary key)
        public int? CustomerId { get; set; } // CustomerId
        public int? EmployeeId { get; set; } // EmployeeId
        public DateTime? OrderDate { get; set; } // OrderDate
        public DateTime? RequiredDate { get; set; } // RequiredDate
        public DateTime? ShippedDate { get; set; } // ShippedDate
        public int? ShipVia { get; set; } // ShipVia
        public decimal? Freight { get; set; } // Freight
        public string ShipName { get; set; } // ShipName
        public string ShipAddress { get; set; } // ShipAddress
        public string ShipCity { get; set; } // ShipCity
        public string ShipRegion { get; set; } // ShipRegion
        public string ShipPostalCode { get; set; } // ShipPostalCode
        public string ShipCountry { get; set; } // ShipCountry

        // Reverse navigation
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } // Order Details.FK_Order_Details_Orders

        // Foreign keys
        public virtual Customer Customer { get; set; } // FK_Orders_Customers
        public virtual Employee Employee { get; set; } // FK_Orders_Employees
        public virtual Shipper Shipper { get; set; } // FK_Orders_Shippers

        public Order()
        {
            Freight = 0m;
            OrderDetails = new List<OrderDetail>();
        }
    }

}
