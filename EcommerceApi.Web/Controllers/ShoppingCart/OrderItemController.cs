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
    public class OrderItemController : BaseCrudController<OrderItemViewModel, OrderItem, OrderItemController>
    {
        public OrderItemController(ILogger<OrderItemController> logger, 
            IGenericRepository<OrderItem> repository, IMapper mapper)
            : base(logger, repository, mapper)
        {
        }
    }
}
