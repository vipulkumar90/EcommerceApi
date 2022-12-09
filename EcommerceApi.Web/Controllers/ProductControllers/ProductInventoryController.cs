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
    public class ProductInventoryController : BaseCrudController<ProductInventoryViewModel,
        ProductInventory, ProductInventoryController>
    {
        public ProductInventoryController(ILogger<ProductInventoryController> logger,
            IGenericRepository<ProductInventory> repository, IMapper mapper)
            : base(logger, repository, mapper)
        {
        }
    }
}
