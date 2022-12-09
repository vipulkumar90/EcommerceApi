using AutoMapper;
using EcommerceApi.DAL.Entities.ProductData;
using EcommerceApi.DAL.Repositories.IRepositories;
using EcommerceApi.Web.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EcommerceApi.Web.Controllers.ProductControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : BaseCrudController<ProductCategoryViewModel,
        ProductCategory, ProductCategoryController>
    {
        public ProductCategoryController(ILogger<ProductCategoryController> logger,
            IGenericRepository<ProductCategory> repository, IMapper mapper)
            : base(logger, repository, mapper)
        {
        }
    }
}
