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
    public class DeliveryPartnerService : IDeliveryPartnerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DeliveryPartnerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DeliveryPartnerResponseDto>> GetAllAsync()
        {
            var items = await _context.DeliveryPartners.ToListAsync();
            return _mapper.Map<IEnumerable<DeliveryPartnerResponseDto>>(items);
        }
        public async Task<DeliveryPartnerResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _context.DeliveryPartners.FindAsync(id);
            return item == null ? null : _mapper.Map<DeliveryPartnerResponseDto>(item);
        }
        public async Task<PaginatedResult<DeliveryPartnerResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            var query = _context.DeliveryPartners.AsQueryable();
            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResult<DeliveryPartnerResponseDto>
            {
                Items = _mapper.Map<IEnumerable<DeliveryPartnerResponseDto>>(items),
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<ApiResponse<DeliveryPartnerResponseDto>> InsertAsync(CreateDeliveryPartnerDto dto)
        {
            var entity = _mapper.Map<DeliveryPartner>(dto);
            _context.DeliveryPartners.Add(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<DeliveryPartnerResponseDto>
            {
                StatusCode = 201,
                Status = true,
                Message = Messages.Success_DeliveryPartner_Created,
                Data = _mapper.Map<DeliveryPartnerResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<DeliveryPartnerResponseDto>> UpdateAsync(Guid id, UpdateDeliveryPartnerDto dto)
        {
            var entity = await _context.DeliveryPartners.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<DeliveryPartnerResponseDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_DeliveryPartner_NotFound,
                    Data = null
                };
            }
            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<DeliveryPartnerResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_DeliveryPartner_Updated,
                Data = _mapper.Map<DeliveryPartnerResponseDto>(entity)
            };
        }
        public async Task<ApiResponse<bool>> DeleteAsync(Guid id)
        {
            var entity = await _context.DeliveryPartners.FindAsync(id);
            if (entity == null)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_DeliveryPartner_NotFound,
                    Data = false
                };
            }
            _context.DeliveryPartners.Remove(entity);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_DeliveryPartner_Deleted,
                Data = true
            };
        }
    }
}
