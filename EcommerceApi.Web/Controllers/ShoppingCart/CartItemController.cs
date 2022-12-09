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
    public class CartItemController : BaseCrudController<CartItemViewModel,
        CartItem, CartItemController>
    {
        public CartItemController(ILogger<CartItemController> logger,
            IGenericRepository<CartItem> repository, IMapper mapper)
            : base(logger, repository, mapper)
        {
        }
    }
}
