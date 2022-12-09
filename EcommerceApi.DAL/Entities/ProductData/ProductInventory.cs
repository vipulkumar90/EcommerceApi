using System;

namespace EcommerceApi.DAL.Entities.ProductData
{
    public class ProductInventory
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
