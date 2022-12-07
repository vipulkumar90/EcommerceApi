using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApi.DAL.Entities.ShopingCart
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        //Fk
        public OrderDetail OrderDetail { get; set; }
        //public Product Product { get; set; }
    }
}
