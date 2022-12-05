using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApi.DAL.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T obj);
        void Update(T obj);
        Task<bool> DeleteAsync(object id);
        Task SaveAsync();
    }
}
