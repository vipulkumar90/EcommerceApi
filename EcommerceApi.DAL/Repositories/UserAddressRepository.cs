using EcommerceApi.DAL.DataContext;
using EcommerceApi.DAL.Entities.User;
using EcommerceApi.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApi.DAL.Repositories
{
    public class UserAddressRepository : GenericRepository<UserAddress>, IUserAddressRepository
    {
        private readonly UserManager<User> userManager;

        public UserAddressRepository(UserManager<User> userManager, EcommerceContext context)
            : base(context)
        {
            this.userManager = userManager;
        }

        public async Task InsertAsync(UserAddress userAddress, string username)
        {
            //Find parent entity
            var user = await userManager.FindByNameAsync(username);
            
            if (user != null)
            {
                //Add child to parent
                if (user.UserAddresses == null)
                {
                    user.UserAddresses = new List<UserAddress> { userAddress };
                }
                else
                {
                    user.UserAddresses.Add(userAddress);
                }
                //Add child
                await InsertAsync(userAddress);
            }
        }
    }
}
