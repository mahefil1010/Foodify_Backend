using Foodify.Data.DTOs;
using Foodify.Core.Models;

namespace Foodify.Service.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemResponseDto>> GetAllAsync();
        Task<OrderItemResponseDto?> GetByIdAsync(Guid id);
        Task<PaginatedResult<OrderItemResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<ApiResponse<OrderItemResponseDto>> InsertAsync(CreateOrderItemDto dto);
        Task<ApiResponse<OrderItemResponseDto>> UpdateAsync(Guid id, UpdateOrderItemDto dto);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
    }
}
