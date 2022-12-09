using EcommerceApi.DAL.Entities.UserData;
using System;

namespace EcommerceApi.DAL.Entities.ShopingCart
{
    public class ShoppingSession
    {
        public Guid Id { get; set; }
        public float Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public User User { get; set; }
        public CartItem CartItem { get; set; }
    }
}
