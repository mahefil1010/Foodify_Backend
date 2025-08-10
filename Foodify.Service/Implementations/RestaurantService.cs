using Foodify.Data.DTOs;
using Foodify.Core.Models;
using Foodify.Data.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Foodify.Data;
using Foodify.Core.Resources;
using Foodify.Service.Interfaces;

namespace Foodify.Service.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public RestaurantService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RestaurantResponseDto>> GetAllAsync()
        {
            var items = await _context.Restaurants.ToListAsync();
            return _mapper.Map<IEnumerable<RestaurantResponseDto>>(items);
        }
        public async Task<RestaurantResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _context.Restaurants.FindAsync(id);
            return item == null ? null : _mapper.Map<RestaurantResponseDto>(item);
        }
        public async Task<PaginatedResult<RestaurantResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            var query = _context.Restaurants.AsQueryable();
            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResult<RestaurantResponseDto>
            {
                Items = _mapper.Map<IEnumerable<RestaurantResponseDto>>(items),
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<ApiResponse<RestaurantResponseDto>> InsertAsync(CreateRestaurantDto dto)
        {
            var entity = _mapper.Map<Restaurant>(dto);
            _context.Restaurants.Add(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<RestaurantResponseDto>
            {
                StatusCode = 201,
                Status = true,
                Message = Messages.Success_Restaurant_Created,
                Data = _mapper.Map<RestaurantResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<RestaurantResponseDto>> UpdateAsync(Guid id, UpdateRestaurantDto dto)
        {
            var entity = await _context.Restaurants.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<RestaurantResponseDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_Restaurant_NotFound,
                    Data = null
                };
            }
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<RestaurantResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_Restaurant_Updated,
                Data = _mapper.Map<RestaurantResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var entity = await _context.Restaurants.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_Restaurant_NotFound,
                    Data = false
                };
            }
            _context.Restaurants.Remove(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_Restaurant_Deleted,
                Data = true
            };
        }
    }
}
