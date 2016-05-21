using System.Collections.Generic;

namespace Ottoman.Entities
{
    using Repository.Pattern.Ef6.Infrastructure;

    // Customers
    public class Customer : BaseEntity<int>
    {
        public string CompanyName { get; set; } // CompanyName
        public string ContactName { get; set; } // ContactName
        public string ContactTitle { get; set; } // ContactTitle
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string Phone { get; set; } // Phone
        public string Fax { get; set; } // Fax

        // Reverse navigation
        public virtual ICollection<Order> Orders { get; set; } // Orders.FK_Orders_Customers

        public Customer()
        {
            Orders = new List<Order>();
        }
    }

}
