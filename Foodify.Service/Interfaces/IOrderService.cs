using Foodify.Data.DTOs;
using Foodify.Core.Models;

namespace Foodify.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseDto>> GetAllAsync();
        Task<OrderResponseDto?> GetByIdAsync(Guid id);
        Task<PaginatedResult<OrderResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<ApiResponse<OrderResponseDto>> InsertAsync(CreateOrderDto dto);
        Task<ApiResponse<OrderResponseDto>> UpdateAsync(Guid id, UpdateOrderDto dto);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
    }
}
