using Microsoft.AspNetCore.Mvc;
using Foodify.Service.Interfaces;
using Foodify.Data.DTOs;
using Foodify.Core.Models;

namespace Foodify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;
        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<RestaurantResponseDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Success",
                Data = result
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound(new ApiResponse<RestaurantResponseDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "Not found",
                    Data = null
                });
            return Ok(new ApiResponse<RestaurantResponseDto>
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
            var result = await _service.GetWithPaginationAsync(pageNumber, pageSize);
            return Ok(new ApiResponse<PaginatedResult<RestaurantResponseDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Success",
                Data = result
            });
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CreateRestaurantDto dto)
        {
            var response = await _service.InsertAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRestaurantDto dto)
        {
            var response = await _service.UpdateAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _service.DeleteAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
