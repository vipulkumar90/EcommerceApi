using System;

namespace EcommerceApi.Web.ViewModels
{
    public class UserPaymentViewModel
    {
        public Guid Id { get; set; }
        public string PaymentType { get; set; }
        public string Provider { get; set; }
        public string AccountNo { get; set; }
        public DateTime Expiry { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
