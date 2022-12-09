using AutoMapper;
using EcommerceApi.DAL.Entities.ShopingCart;
using EcommerceApi.DAL.Repositories.IRepositories;
using EcommerceApi.Web.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Web.Controllers.ShoppingCart
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingSessionController : BaseCrudController<ShoppingSessionViewModel,
        ShoppingSession, ShoppingSessionController>
    {
        public ShoppingSessionController(ILogger<ShoppingSessionController> logger, 
            IGenericRepository<ShoppingSession> repository, IMapper mapper) : 
            base(logger, repository, mapper)
        {
        }
    }
}
