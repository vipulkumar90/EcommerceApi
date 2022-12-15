using AutoMapper;
using EcommerceApi.DAL.Entities.ProductData;
using EcommerceApi.DAL.Repositories.IRepositories;
using EcommerceApi.Web.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApi.Web.Controllers.ProductControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseCrudController<ProductViewModel, Product, ProductController>
    {
        private readonly IProductRepository repository;

        public ProductController(ILogger<ProductController> logger,
            IMapper mapper,
            IProductRepository repository)
            : base(logger, repository, mapper)
        {
            this.repository = repository;
        }
        [HttpGet("bycategory")]
        public async Task<IActionResult> ByCategory(IList<string> categoryList)
        {
            return Ok(await repository.CategorySpecificProduct(categoryList));
        }
        [HttpGet("byprice")]
        public async Task<IActionResult> ByPrice(object priceRangeList)
        {
            throw new NotImplementedException();
        }
    }
}
