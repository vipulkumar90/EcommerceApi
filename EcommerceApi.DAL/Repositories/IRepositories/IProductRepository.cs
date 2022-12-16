using EcommerceApi.DAL.Entities.ProductData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApi.DAL.Repositories.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //Price range
        Task<IEnumerable<Product>> PriceRange(IList<IList<double>> priceRangeList);
        //Category
        Task<IEnumerable<Product>> CategorySpecificProduct(IList<string> categoryList);
    }
}
