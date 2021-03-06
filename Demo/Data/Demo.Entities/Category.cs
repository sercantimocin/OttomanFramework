namespace Demo.Entities
{
    using System.Collections.Generic;

    using Ottoman.Core.Data;

    // Categories
    public class Category : BaseEntity<int>
    {
        public string CategoryName { get; set; } // CategoryName
        public string Description { get; set; } // Description
        public byte[] Picture { get; set; } // Picture

        // Reverse navigation
        public virtual ICollection<Product> Products { get; set; } // Products.FK_Products_Categories

        public Category()
        {
            this.Products = new List<Product>();
        }
    }

}
