using System;

namespace EcommerceApi.Web.ViewModels.ProductViewModels
{
    public class ProductInventoryViewModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
