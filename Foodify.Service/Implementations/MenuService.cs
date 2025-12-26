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
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public MenuService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MenuResponseDto>> GetAllAsync()
        {
            var items = await _context.Menus.ToListAsync();
            return _mapper.Map<IEnumerable<MenuResponseDto>>(items);
        }
        public async Task<MenuResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _context.Menus.FindAsync(id);
            return item == null ? null : _mapper.Map<MenuResponseDto>(item);
        }
        public async Task<PaginatedResult<MenuResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            var query = _context.Menus.AsQueryable();
            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResult<MenuResponseDto>
            {
                Items = _mapper.Map<IEnumerable<MenuResponseDto>>(items),
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<ApiResponse<MenuResponseDto>> InsertAsync(CreateMenuDto dto)
        {
            var entity = _mapper.Map<Menu>(dto);
            _context.Menus.Add(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<MenuResponseDto>
            {
                StatusCode = 201,
                Status = true,
                Message = Messages.Success_Menu_Created,
                Data = _mapper.Map<MenuResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<MenuResponseDto>> UpdateAsync(Guid id, UpdateMenuDto dto)
        {
            var entity = await _context.Menus.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<MenuResponseDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_Menu_NotFound,
                    Data = null
                };
            }
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<MenuResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_Menu_Updated,
                Data = _mapper.Map<MenuResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var entity = await _context.Menus.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_Menu_NotFound,
                    Data = false
                };
            }
            _context.Menus.Remove(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_Menu_Deleted,
                Data = true
            };
        }
    }
}
