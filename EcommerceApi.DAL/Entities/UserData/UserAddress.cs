using System;

namespace EcommerceApi.DAL.Entities.UserData
{
    public class UserAddress
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public User User { get; set; }
    }
}
