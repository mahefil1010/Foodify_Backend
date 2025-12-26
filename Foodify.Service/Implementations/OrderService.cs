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
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<OrderResponseDto>> GetAllAsync()
        {
            var items = await _context.Orders.ToListAsync();
            return _mapper.Map<IEnumerable<OrderResponseDto>>(items);
        }
        public async Task<OrderResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _context.Orders.FindAsync(id);
            return item == null ? null : _mapper.Map<OrderResponseDto>(item);
        }
        public async Task<PaginatedResult<OrderResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            var query = _context.Orders.AsQueryable();
            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResult<OrderResponseDto>
            {
                Items = _mapper.Map<IEnumerable<OrderResponseDto>>(items),
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<ApiResponse<OrderResponseDto>> InsertAsync(CreateOrderDto dto)
        {
            var entity = _mapper.Map<Order>(dto);
            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<OrderResponseDto>
            {
                StatusCode = 201,
                Status = true,
                Message = Messages.Success_Order_Created,
                Data = _mapper.Map<OrderResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<OrderResponseDto>> UpdateAsync(Guid id, UpdateOrderDto dto)
        {
            var entity = await _context.Orders.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<OrderResponseDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_Order_NotFound,
                    Data = null
                };
            }
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<OrderResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_Order_Updated,
                Data = _mapper.Map<OrderResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var entity = await _context.Orders.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_Order_NotFound,
                    Data = false
                };
            }
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_Order_Deleted,
                Data = true
            };
        }
    }
}
