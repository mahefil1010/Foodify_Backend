using Foodify.Data.DTOs;
using Foodify.Core.Models;

namespace Foodify.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task<PaginatedResult<UserResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize);
        Task<ApiResponse<UserResponseDto>> InsertAsync(CreateUserDto dto);
        Task<ApiResponse<UserResponseDto>> UpdateAsync(int id, UpdateUserDto dto);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
