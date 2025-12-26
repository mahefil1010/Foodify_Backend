using Microsoft.AspNetCore.Mvc;
using Foodify.Service.Interfaces;
using Foodify.Data.DTOs;
using Foodify.Core.Models;

namespace Foodify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<UserResponseDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Success",
                Data = result
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new ApiResponse<UserResponseDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "User not found",
                    Data = null
                });
            return Ok(new ApiResponse<UserResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Success",
                Data = result
            });
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetWithPagination([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _userService.GetWithPaginationAsync(pageNumber, pageSize);
            return Ok(new ApiResponse<PaginatedResult<UserResponseDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Success",
                Data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CreateUserDto dto)
        {
            var response = await _userService.InsertAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var response = await _userService.UpdateAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _userService.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
