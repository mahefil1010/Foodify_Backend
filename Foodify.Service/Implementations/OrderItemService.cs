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
    public class OrderItemService : IOrderItemService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderItemService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderItemResponseDto>> GetAllAsync()
        {
            var items = await _context.OrderItems.ToListAsync();
            return _mapper.Map<IEnumerable<OrderItemResponseDto>>(items);
        }
        public async Task<OrderItemResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _context.OrderItems.FindAsync(id);
            return item == null ? null : _mapper.Map<OrderItemResponseDto>(item);
        }
        public async Task<PaginatedResult<OrderItemResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            var query = _context.OrderItems.AsQueryable();
            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResult<OrderItemResponseDto>
            {
                Items = _mapper.Map<IEnumerable<OrderItemResponseDto>>(items),
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<ApiResponse<OrderItemResponseDto>> InsertAsync(CreateOrderItemDto dto)
        {
            var entity = _mapper.Map<OrderItem>(dto);
            _context.OrderItems.Add(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<OrderItemResponseDto>
            {
                StatusCode = 201,
                Status = true,
                Message = Messages.Success_OrderItem_Created,
                Data = _mapper.Map<OrderItemResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<OrderItemResponseDto>> UpdateAsync(Guid id, UpdateOrderItemDto dto)
        {
            var entity = await _context.OrderItems.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<OrderItemResponseDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_OrderItem_NotFound,
                    Data = null
                };
            }
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<OrderItemResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_OrderItem_Updated,
                Data = _mapper.Map<OrderItemResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var entity = await _context.OrderItems.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_OrderItem_NotFound,
                    Data = false
                };
            }
            _context.OrderItems.Remove(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_OrderItem_Deleted,
                Data = true
            };
        }
    }
}
