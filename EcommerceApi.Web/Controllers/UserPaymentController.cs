using AutoMapper;
using EcommerceApi.DAL.Entities.UserData;
using EcommerceApi.DAL.Repositories.IRepositories;
using EcommerceApi.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcommerceApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserPaymentController : BaseUserController<UserPaymentViewModel, UserPayment, UserPaymentController>
    {
        public UserPaymentController(ILogger<UserPaymentController> logger,
            IUserPaymentRepository repository,
            IMapper mapper)
            :base(logger, repository, mapper)
        {
        }
    }
}
