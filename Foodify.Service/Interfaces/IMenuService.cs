using Foodify.Data.DTOs;
using Foodify.Core.Models;

namespace Foodify.Service.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuResponseDto>> GetAllAsync();
        Task<MenuResponseDto?> GetByIdAsync(Guid id);
        Task<PaginatedResult<MenuResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<ApiResponse<MenuResponseDto>> InsertAsync(CreateMenuDto dto);
        Task<ApiResponse<MenuResponseDto>> UpdateAsync(Guid id, UpdateMenuDto dto);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
    }
}
