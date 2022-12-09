using System;

namespace EcommerceApi.DAL.Entities.ProductData
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public Discount Discount { get; set; }
        public ProductInventory ProductInventory { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
