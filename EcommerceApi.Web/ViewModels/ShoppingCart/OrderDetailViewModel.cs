using System;

namespace EcommerceApi.Web.ViewModels.ShoppingCart
{
    public class OrderDetailViewModel
    {
        public Guid Id { get; set; }
        public float Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
