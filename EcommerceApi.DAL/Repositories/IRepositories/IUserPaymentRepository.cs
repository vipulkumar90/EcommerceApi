using EcommerceApi.DAL.Entities.UserData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApi.DAL.Repositories.IRepositories
{
    public interface IUserPaymentRepository : IGenericRepository<UserPayment>
    {
        Task InsertAsync(UserPayment userPayment, string username);
        Task<IEnumerable<UserPayment>> GetAllForUserAsync(string userId);
    }
}
