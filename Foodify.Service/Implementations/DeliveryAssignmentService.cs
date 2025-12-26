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
    public class DeliveryAssignmentService : IDeliveryAssignmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeliveryAssignmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DeliveryAssignmentResponseDto>> GetAllAsync()
        {
            var items = await _context.DeliveryAssignments.ToListAsync();
            return _mapper.Map<IEnumerable<DeliveryAssignmentResponseDto>>(items);
        }
        public async Task<DeliveryAssignmentResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _context.DeliveryAssignments.FindAsync(id);
            return item == null ? null : _mapper.Map<DeliveryAssignmentResponseDto>(item);
        }
        public async Task<PaginatedResult<DeliveryAssignmentResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            var query = _context.DeliveryAssignments.AsQueryable();
            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResult<DeliveryAssignmentResponseDto>
            {
                Items = _mapper.Map<IEnumerable<DeliveryAssignmentResponseDto>>(items),
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<ApiResponse<DeliveryAssignmentResponseDto>> InsertAsync(CreateDeliveryAssignmentDto dto)
        {
            var entity = _mapper.Map<DeliveryAssignment>(dto);
            _context.DeliveryAssignments.Add(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<DeliveryAssignmentResponseDto>
            {
                StatusCode = 201,
                Status = true,
                Message = Messages.Success_DeliveryAssignment_Created,
                Data = _mapper.Map<DeliveryAssignmentResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<DeliveryAssignmentResponseDto>> UpdateAsync(Guid id, UpdateDeliveryAssignmentDto dto)
        {
            var entity = await _context.DeliveryAssignments.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<DeliveryAssignmentResponseDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_DeliveryAssignment_NotFound,
                    Data = null
                };
            }
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<DeliveryAssignmentResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_DeliveryAssignment_Updated,
                Data = _mapper.Map<DeliveryAssignmentResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var entity = await _context.DeliveryAssignments.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_DeliveryAssignment_NotFound,
                    Data = false
                };
            }
            _context.DeliveryAssignments.Remove(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_DeliveryAssignment_Deleted,
                Data = true
            };
        }
    }
}
