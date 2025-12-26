using System.Threading.Tasks;
using Foodify.Data.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Foodify.Service.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto model, string role);
        Task<LoginResultDto> LoginAsync(LoginDto model);
        Task<bool> ForgotPasswordAsync(string email);
    }
}
