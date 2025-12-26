using Foodify.Data.DTOs;
using Foodify.Service.Interfaces;
using Foodify.Core.Models;
using Foodify.Data.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Foodify.Data;
using Foodify.Core.Resources;

namespace Foodify.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserResponseDto>>(users);
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? null : _mapper.Map<UserResponseDto>(user);
        }

        public async Task<PaginatedResult<UserResponseDto>> GetWithPaginationAsync(int pageNumber, int pageSize)
        {
            var query = _context.Users.AsQueryable();
            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedResult<UserResponseDto>
            {
                Items = _mapper.Map<IEnumerable<UserResponseDto>>(items),
                TotalCount = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ApiResponse<UserResponseDto>> InsertAsync(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new ApiResponse<UserResponseDto>
            {
                StatusCode = 201,
                Status = true,
                Message = Messages.Success_User_Created,
                Data = _mapper.Map<UserResponseDto>(user)
            };
        }

        public async Task<ApiResponse<UserResponseDto>> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new ApiResponse<UserResponseDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_User_NotFound,
                    Data = null
                };
            }
            _mapper.Map(dto, user);
            await _context.SaveChangesAsync();
            return new ApiResponse<UserResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_User_Updated,
                Data = _mapper.Map<UserResponseDto>(user)
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new ApiResponse<bool>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = Messages.Error_User_NotFound,
                    Data = false
                };
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                StatusCode = 200,
                Status = true,
                Message = Messages.Success_User_Deleted,
                Data = true
            };
        }
    }
}
