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
        public async Task<IActionResult> ByPrice(IList<string> priceRanges)
        {
            IList<IList<double>> priceRangeList = new List<IList<double>>();
            foreach (var priceRange in priceRanges)
            {
                var priceString = priceRange.Split(',');
                var startPrice = double.Parse(priceString[0]);
                var endPrice = double.Parse(priceString[1]);
                priceRangeList.Add(new List<double> { startPrice, endPrice });
            }
            return Ok(await repository.PriceRange(priceRangeList));
        }
    }
}
