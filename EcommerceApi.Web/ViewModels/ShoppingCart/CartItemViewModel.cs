using System;

namespace EcommerceApi.Web.ViewModels.ShoppingCart
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
