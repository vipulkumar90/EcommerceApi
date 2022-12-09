using AutoMapper;
using EcommerceApi.DAL.Entities.ShopingCart;
using EcommerceApi.DAL.Repositories.IRepositories;
using EcommerceApi.Web.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EcommerceApi.Web.Controllers.ShoppingCart
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : BaseCrudController<OrderDetailViewModel, 
        OrderDetail, OrderDetailController>
    {
        public OrderDetailController(ILogger<OrderDetailController> logger,
            IGenericRepository<OrderDetail> repository, IMapper mapper)
            : base(logger, repository, mapper)
        {
        }
    }
}
