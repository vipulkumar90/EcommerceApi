using AutoMapper;
using EcommerceApi.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseCrudController<TViewModel, TEntity, TController> : ControllerBase
        where TViewModel : class
        where TEntity : class
        where TController : class
    {
        private readonly ILogger<TController> logger;
        private readonly IGenericRepository<TEntity> repository;
        private readonly IMapper mapper;

        public BaseCrudController(ILogger<TController> logger,
            IUserUtilRepository<TEntity> repository,
            IMapper mapper)
        {
            this.logger = logger;
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<TViewModel>> GetAsync() =>
            mapper.Map<IEnumerable<TEntity>, IEnumerable<TViewModel>>(await repository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<TViewModel> GetAsync([FromRoute] Guid id) =>
            mapper.Map<TEntity, TViewModel>(await repository.GetByIdAsync(id));

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync([FromBody] TViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var modelToEntity = mapper.Map<TViewModel, TEntity>(viewModel);
                await repository.InsertAsync(modelToEntity);
                await repository.SaveAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task PutAsync([FromBody] TViewModel viewModel)
        {
            var entity = mapper.Map<TViewModel, TEntity>(viewModel);
            repository.Update(entity);
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
