using AutoMapper;
using Foodify.Data.Entities;
using Foodify.Data.DTOs;

namespace Foodify.Data.Mappers
{
    public class DeliveryPartnerProfile : Profile
    {
        public DeliveryPartnerProfile()
        {
            CreateMap<DeliveryPartner, DeliveryPartnerResponseDto>();
            CreateMap<CreateDeliveryPartnerDto, DeliveryPartner>();
            CreateMap<UpdateDeliveryPartnerDto, DeliveryPartner>();
        }
    }
}
