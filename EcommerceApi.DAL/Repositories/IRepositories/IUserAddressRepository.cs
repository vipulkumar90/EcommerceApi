using EcommerceApi.DAL.Entities.UserData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApi.DAL.Repositories.IRepositories
{
    public interface IUserAddressRepository: IGenericRepository<UserAddress>
    {
        Task InsertAsync(UserAddress userAddress, string username);
        Task<IEnumerable<UserAddress>> GetAllForUserAsync(string userId);
    }
}
