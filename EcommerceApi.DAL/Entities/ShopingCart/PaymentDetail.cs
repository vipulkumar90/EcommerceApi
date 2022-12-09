using System;

namespace EcommerceApi.DAL.Entities.ShopingCart
{
    public class PaymentDetail
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string Provider { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
