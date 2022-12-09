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
    public class PaymentDetailController : BaseCrudController<PaymentDetailViewModel, 
        PaymentDetail, PaymentDetailController>
    {
        public PaymentDetailController(ILogger<PaymentDetailController> logger, 
            IGenericRepository<PaymentDetail> repository, IMapper mapper)
            : base(logger, repository, mapper)
        {
        }
    }
}
