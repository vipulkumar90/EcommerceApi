using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApi.DAL.Repositories.IRepositories
{
    public interface IUserUtilRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        Task InsertAsync(TEntity entity, string username);
        Task<IEnumerable<TEntity>> GetAllForUserAsync(string userId);
    }
}
