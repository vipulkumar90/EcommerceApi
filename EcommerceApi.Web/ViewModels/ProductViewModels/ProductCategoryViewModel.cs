using System;

namespace EcommerceApi.Web.ViewModels.ProductViewModels
{
    public class ProductCategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
