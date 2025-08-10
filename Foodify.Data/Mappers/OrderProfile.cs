using AutoMapper;
using Foodify.Data.Entities;
using Foodify.Data.DTOs;

namespace Foodify.Data.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderResponseDto>();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<UpdateOrderDto, Order>();
        }
    }
}
