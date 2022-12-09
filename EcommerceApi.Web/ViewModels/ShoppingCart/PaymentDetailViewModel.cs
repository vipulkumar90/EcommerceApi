using System;

namespace EcommerceApi.Web.ViewModels.ShoppingCart
{
    public class PaymentDetailViewModel
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string Provider { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
