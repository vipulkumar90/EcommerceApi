using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceApi.DAL.Entities.UserData
{
    public class UserPayment
    {
        public Guid Id { get; set; }
        public string PaymentType { get; set; }
        public string Provider { get; set; }
        public string AccountNo { get; set; }
        public DateTime Expiry { get; set; }
        public User User { get; set; }
    }
}
