using EcommerceApi.DAL.Entities.UserData;
using System;

namespace EcommerceApi.DAL.Entities.ShopingCart
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public float Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        //Fk
        public User User { get; set; }
        public PaymentDetail PaymentDetail { get; set; }
    }
}
