using AutoMapper;
using Foodify.Data.Entities;
using Foodify.Data.DTOs;

namespace Foodify.Data.Mappers
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemResponseDto>();
            CreateMap<CreateOrderItemDto, OrderItem>();
            CreateMap<UpdateOrderItemDto, OrderItem>();
        }
    }
}
