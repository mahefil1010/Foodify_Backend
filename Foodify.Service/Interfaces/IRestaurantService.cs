using Foodify.Data.DTOs;
using Foodify.Core.Models;

namespace Foodify.Service.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantResponseDto>> GetAllAsync();
        Task<RestaurantResponseDto?> GetByIdAsync(Guid id);
        Task<PaginatedResult<RestaurantResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<ApiResponse<RestaurantResponseDto>> InsertAsync(CreateRestaurantDto dto);
        Task<ApiResponse<RestaurantResponseDto>> UpdateAsync(Guid id, UpdateRestaurantDto dto);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
    }
}
