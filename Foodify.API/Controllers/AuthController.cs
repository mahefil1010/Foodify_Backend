using System.Threading.Tasks;
using Foodify.Data.DTOs;
using Foodify.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Foodify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AuthController(AuthService authService, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _authService = authService;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!await _roleManager.RoleExistsAsync(model.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(model.Role));
            }
            var result = await _authService.RegisterAsync(model, model.Role);
            if (result.Succeeded)
                return Ok("Registration successful");
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authService.LoginAsync(model);
            if (result == null)
                return Unauthorized("Invalid credentials");
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var result = await _authService.ForgotPasswordAsync(email);
            if (!result)
                return NotFound("User not found");
            return Ok("Password reset instructions sent (mock)");
        }
    }
}
