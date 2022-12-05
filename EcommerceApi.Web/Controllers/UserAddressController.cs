using AutoMapper;
using EcommerceApi.DAL.Entities.User;
using EcommerceApi.DAL.Repositories.IRepositories;
using EcommerceApi.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAddressController : ControllerBase
    {
        private readonly ILogger<UserAddressController> logger;
        private readonly IUserAddressRepository repository;
        private readonly IMapper mapper;

        public UserAddressController(ILogger<UserAddressController> logger,
            IUserAddressRepository repository,
            IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserAddressViewModel>> GetAsync() =>
            mapper.Map<IEnumerable<UserAddress>, IEnumerable<UserAddressViewModel>>(await repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<UserAddressViewModel> GetAsync([FromRoute] Guid id) =>
            mapper.Map<UserAddress, UserAddressViewModel>(await repository.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserAddressViewModel userAddressViewModel)
        {
            if (ModelState.IsValid)
            {
                var username = User.Claims.FirstOrDefault(x => x.Type.Equals(System.Security.Claims.ClaimTypes.Name)).Value;
                if (username != null)
                {
                    var modelToDto = mapper.Map<UserAddressViewModel, UserAddress>(userAddressViewModel);

                    await repository.InsertAsync(modelToDto, username);
                    await repository.SaveAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task PutAsync([FromRoute] Guid id, [FromBody] UserAddressViewModel userAddressViewModel)
        {
            var userAddress = mapper.Map<UserAddressViewModel, UserAddress>(userAddressViewModel);
            userAddress.Id = id;
            await repository.UpdateAsync(id, userAddress);
            await repository.SaveAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync([FromRoute] Guid id)
        {
            await repository.DeleteAsync(id);
            await repository.SaveAsync();
        }
    }
}
