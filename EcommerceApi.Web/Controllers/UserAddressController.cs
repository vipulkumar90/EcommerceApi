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
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net;

namespace EcommerceApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAddressController : BaseUserController<UserAddressViewModel, UserAddress, UserAddressController>
    {
        public UserAddressController(ILogger<UserAddressController> logger,
            IUserAddressRepository repository,
            IMapper mapper)
            : base(logger, repository, mapper)
        {
        }
    }
}
