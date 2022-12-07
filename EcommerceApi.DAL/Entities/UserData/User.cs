using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EcommerceApi.DAL.Entities.UserData
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        //Foreign Key
        public List<UserAddress> UserAddresses { get; set; }
        public List<UserPayment> UserPayments { get; set; }
    }
}
