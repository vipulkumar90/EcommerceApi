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
    public class UserPaymentController : ControllerBase
    {
        private readonly ILogger<UserPaymentController> logger;
        private readonly IUserPaymentRepository repository;
        private readonly IMapper mapper;

        public UserPaymentController(ILogger<UserPaymentController> logger,
            IUserPaymentRepository repository,
            IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }
        //Admin: get all payment
        //[Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IEnumerable<UserPaymentViewModel>> GetAsync() =>
            mapper.Map<IEnumerable<UserPayment>, IEnumerable<UserPaymentViewModel>>(await repository.GetAllAsync());
        //[Authorize(Roles = "customer")]
        [HttpGet("user")]
        public async Task<IActionResult> GetForUserAsync()
        {
            //Get the userid
            var userId = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            if (userId == null)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, new { message = "no userid found" });
            }
            var entityToModel = mapper.Map<IEnumerable<UserPayment>,
                IEnumerable<UserPaymentViewModel>>(await repository.GetAllForUserAsync(userId));
            return Ok(entityToModel);
        }

        [HttpGet("{id}")]
        public async Task<UserPaymentViewModel> GetAsync([FromRoute] Guid id) =>
            mapper.Map<UserPayment, UserPaymentViewModel>(await repository.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserPaymentViewModel userPaymentViewModel)
        {
            if (ModelState.IsValid)
            {
                var username = User.Claims.FirstOrDefault(x => x.Type.Equals(System.Security.Claims.ClaimTypes.Name)).Value;
                if (username != null)
                {
                    var modelToEntity = mapper.Map<UserPaymentViewModel, UserPayment>(userPaymentViewModel);

                    await repository.InsertAsync(modelToEntity, username);
                    await repository.SaveAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task PutAsync([FromRoute] Guid id, [FromBody] UserPaymentViewModel userPaymentViewModel)
        {
            var userPayment = mapper.Map<UserPaymentViewModel, UserPayment>(userPaymentViewModel);
            userPayment.Id = id;
            repository.Update(userPayment);
            await repository.SaveAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var isPresent = await repository.DeleteAsync(id);
            if (isPresent == false)
            {
                return BadRequest(new { message = "id is not present" });
            }
            await repository.SaveAsync();
            return Ok();
        }
    }
}
