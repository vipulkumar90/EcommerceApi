using EcommerceApi.DAL.DataContext;
using EcommerceApi.DAL.Entities.UserData;
using EcommerceApi.DAL.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.DAL.Repositories
{
    public class UserPaymentRepository : GenericRepository<UserPayment>, IUserPaymentRepository
    {
        private readonly UserManager<User> userManager;

        public UserPaymentRepository(UserManager<User> userManager, EcommerceContext context)
            : base(context)
        {
            this.userManager = userManager;
        }
        public async Task<IEnumerable<UserPayment>> GetAllForUserAsync(string userId) =>
            await table.Where(x => x.User.Id == userId).ToListAsync();
        public async Task InsertAsync(UserPayment userPayment, string username)
        {
            //Find parent entity
            var user = await userManager.FindByNameAsync(username);

            if (user != null)
            {
                //Add child to parent
                if (user.UserPayments == null)
                {
                    user.UserPayments = new List<UserPayment> { userPayment };
                }
                else
                {
                    user.UserPayments.Add(userPayment);
                }
                //Add child
                await InsertAsync(userPayment);
            }
        }
    }
}
