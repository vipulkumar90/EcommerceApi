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
    public class DiscountController : BaseCrudController<DiscountViewModel,
        Discount, DiscountController>
    {
        public DiscountController(ILogger<DiscountController> logger,
            IGenericRepository<Discount> repository, IMapper mapper)
            : base(logger, repository, mapper)
        {
        }
    }
}
