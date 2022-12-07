using AutoMapper;
using EcommerceApi.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class BaseUserController<TViewModel, TEntity, TController> : BaseCrudController<TViewModel, TEntity, TController>
        where TViewModel : class
        where TEntity : class
        where TController : class
    {
        private readonly ILogger<TController> logger;
        private readonly IUserUtilRepository<TEntity> repository;
        private readonly IMapper mapper;

        public BaseUserController(ILogger<TController> logger,
            IUserUtilRepository<TEntity> repository,
            IMapper mapper)
            : base(logger, repository, mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }
        //[Authorize(Roles = "customer")]
        [HttpGet("user")]
        public async Task<IActionResult> GetForUserAsync()
        {
            //Get the userid
            var userId = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            if (userId == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "no userid found" });
            }
            var entityToModel = mapper.Map<IEnumerable<TEntity>,
                IEnumerable<TViewModel>>(await repository.GetAllForUserAsync(userId));
            return Ok(entityToModel);
        }
        [HttpPost]
        public override async Task<IActionResult> PostAsync([FromBody] TViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var username = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value;
                if (username != null)
                {
                    var modelToEntity = mapper.Map<TViewModel, TEntity>(viewModel);

                    await repository.InsertAsync(modelToEntity, username);
                    await repository.SaveAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
