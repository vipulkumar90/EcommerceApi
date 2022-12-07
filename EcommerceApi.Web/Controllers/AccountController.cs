using AutoMapper;
using EcommerceApi.BLL.Services.IServices;
using EcommerceApi.DAL.DataContext;
using EcommerceApi.DAL.Entities.UserData;
using EcommerceApi.Shared.Dtos;
using EcommerceApi.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        private readonly EcommerceContext ecommerceContext;

        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger,
            IMapper mapper,
            IAuthService authService,
            EcommerceContext ecommerceContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.mapper = mapper;
            this.authService = authService;
            this.ecommerceContext = ecommerceContext;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel userViewModel)
        {
            logger.LogInformation($"Registration attempt of {userViewModel.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = mapper.Map<UserViewModel, User>(userViewModel);
                var result = await userManager.CreateAsync(user, userViewModel.Password);
                if (!result.Succeeded)
                {

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await userManager.AddToRoleAsync(user, "customer");

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error: {nameof(Register)}");
                return StatusCode(500);
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LogInViewModel logInViewModel)
        {
            logger.LogInformation($"Login attempt of {logInViewModel.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var logInDto = mapper.Map<LogInViewModel, LogInDto>(logInViewModel);
                if (!await authService.ValidateUser(logInDto))
                {
                    return Unauthorized(new { Message = "Email or password is incorrect" });
                }
                var token = await authService.CreateToken();
                return Accepted(new { Token = await authService.CreateToken() });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error: {nameof(Login)}");
                return StatusCode(500);
            }
        }
        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser()
        {
            var username = User.Claims.FirstOrDefault(x => x.Type.Equals(System.Security.Claims.ClaimTypes.Name));
            if (username == null)
            {
                return BadRequest();
            }
            var user = await userManager.FindByNameAsync(username.Value);
            if (user == null)
            {
                return BadRequest("user not found");
            }
            try
            {
                await userManager.DeleteAsync(user);
                return Ok("User Deleted");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
