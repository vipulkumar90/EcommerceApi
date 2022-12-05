using EcommerceApi.DAL.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApi.DAL.Repositories.IRepositories
{
    public interface IUserAddressRepository: IGenericRepository<UserAddress>
    {
        Task InsertAsync(UserAddress userAddress, string username);
    }
}
