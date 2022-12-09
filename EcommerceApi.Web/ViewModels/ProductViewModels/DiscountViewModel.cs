using System;

namespace EcommerceApi.Web.ViewModels.ProductViewModels
{
    public class DiscountViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double DiscountPercent { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
