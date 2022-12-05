using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Web.ViewModels
{
    public class UserPaymentViewModel
    {
        public Guid Id { get; set; }
        public string PaymentType { get; set; }
        public string Provider { get; set; }
        public string AccountNo { get; set; }
        public DateTime Expiry { get; set; }
    }
}
