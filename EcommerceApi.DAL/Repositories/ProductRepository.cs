using EcommerceApi.DAL.DataContext;
using EcommerceApi.DAL.Entities.ProductData;
using EcommerceApi.DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApi.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(EcommerceContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> CategorySpecificProduct(IList<string> categoryList) => 
            await table.Where(product => categoryList.Any(category => 
            category.Equals(product.ProductCategory.Name, StringComparison.OrdinalIgnoreCase))).ToListAsync();


        public async Task<IEnumerable<Product>> PriceRange(IDictionary<int, int> priceRangeList)
        {
            return await table.Where(product => priceRangeList.Any(priceRange => product.Price >= priceRange.Key && product.Price <= priceRange.Value)).ToListAsync();
        }
    }
}
