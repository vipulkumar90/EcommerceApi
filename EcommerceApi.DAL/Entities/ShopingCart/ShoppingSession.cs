using EcommerceApi.DAL.Entities.UserData;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApi.DAL.Entities.ShopingCart
{
    public class ShoppingSession
    {
        public Guid Id { get; set; }
        public float Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public User User { get; set; }
    }
}
