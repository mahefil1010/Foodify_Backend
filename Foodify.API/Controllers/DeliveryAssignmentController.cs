using Microsoft.AspNetCore.Mvc;
using Foodify.Service.Interfaces;
using Foodify.Data.DTOs;
using Foodify.Core.Models;

namespace Foodify.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryAssignmentController : ControllerBase
    {
        private readonly IDeliveryAssignmentService _service;
        public DeliveryAssignmentController(IDeliveryAssignmentService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<DeliveryAssignmentResponseDto>>
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
                return NotFound(new ApiResponse<DeliveryAssignmentResponseDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "Not found",
                    Data = null
                });
            return Ok(new ApiResponse<DeliveryAssignmentResponseDto>
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
            return Ok(new ApiResponse<PaginatedResult<DeliveryAssignmentResponseDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Success",
                Data = result
            });
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CreateDeliveryAssignmentDto dto)
        {
            var response = await _service.InsertAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDeliveryAssignmentDto dto)
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
