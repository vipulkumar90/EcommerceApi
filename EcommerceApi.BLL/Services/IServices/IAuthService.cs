using EcommerceApi.Shared.Dtos;
using System.Threading.Tasks;

namespace EcommerceApi.BLL.Services.IServices
{
    public interface IAuthService
    {
        Task<string> CreateToken();
        Task<bool> ValidateUser(LogInDto logInDto);
    }
}
