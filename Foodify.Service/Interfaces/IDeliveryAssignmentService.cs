using Foodify.Data.DTOs;
using Foodify.Core.Models;

namespace Foodify.Service.Interfaces
{
    public interface IDeliveryAssignmentService
    {
        Task<IEnumerable<DeliveryAssignmentResponseDto>> GetAllAsync();
        Task<DeliveryAssignmentResponseDto?> GetByIdAsync(Guid id);
        Task<PaginatedResult<DeliveryAssignmentResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<ApiResponse<DeliveryAssignmentResponseDto>> InsertAsync(CreateDeliveryAssignmentDto dto);
        Task<ApiResponse<DeliveryAssignmentResponseDto>> UpdateAsync(Guid id, UpdateDeliveryAssignmentDto dto);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
    }
}
