using Foodify.Data.DTOs;
using Foodify.Core.Models;

namespace Foodify.Service.Interfaces
{
    public interface IDeliveryPartnerService
    {
        Task<IEnumerable<DeliveryPartnerResponseDto>> GetAllAsync();
        Task<DeliveryPartnerResponseDto?> GetByIdAsync(Guid id);
        Task<PaginatedResult<DeliveryPartnerResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<ApiResponse<DeliveryPartnerResponseDto>> InsertAsync(CreateDeliveryPartnerDto dto);
        Task<ApiResponse<DeliveryPartnerResponseDto>> UpdateAsync(Guid id, UpdateDeliveryPartnerDto dto);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
    }
}
