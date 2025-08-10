using AutoMapper;
using Foodify.Data.Entities;
using Foodify.Data.DTOs;

namespace Foodify.Data.Mappers
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuResponseDto>();
            CreateMap<CreateMenuDto, Menu>();
            CreateMap<UpdateMenuDto, Menu>();
        }
    }
}
