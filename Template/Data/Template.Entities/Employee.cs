using System;
using System.Collections.Generic;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace Ottoman.Entities
{
    using Repository.Pattern.Ef6.Infrastructure;

    // Employees
    public class Employee : BaseEntity<int>
    {
        public int Id { get; set; } // Id (Primary key)
        public string LastName { get; set; } // LastName
        public string FirstName { get; set; } // FirstName
        public string Title { get; set; } // Title
        public string TitleOfCourtesy { get; set; } // TitleOfCourtesy
        public DateTime? BirthDate { get; set; } // BirthDate
        public DateTime? HireDate { get; set; } // HireDate
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Region { get; set; } // Region
        public string PostalCode { get; set; } // PostalCode
        public string Country { get; set; } // Country
        public string HomePhone { get; set; } // HomePhone
        public string Extension { get; set; } // Extension
        public byte[] Photo { get; set; } // Photo
        public string Notes { get; set; } // Notes
        public int? ReportsTo { get; set; } // ReportsTo
        public string PhotoPath { get; set; } // PhotoPath

        // Reverse navigation
        public virtual ICollection<Employee> Employees { get; set; } // Employees.FK_Employees_Employees
        public virtual ICollection<Order> Orders { get; set; } // Orders.FK_Orders_Employees
        public virtual ICollection<Territory> Territories { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Employee Employee_ReportsTo { get; set; } // FK_Employees_Employees

        public Employee()
        {
            Employees = new List<Employee>();
            Orders = new List<Order>();
            Territories = new List<Territory>();
        }
    }

}
