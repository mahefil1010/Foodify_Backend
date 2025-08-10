using AutoMapper;
using Foodify.Data.Entities;
using Foodify.Data.DTOs;

namespace Foodify.Data.Mappers
{
    public class DeliveryAssignmentProfile : Profile
    {
        public DeliveryAssignmentProfile()
        {
            CreateMap<DeliveryAssignment, DeliveryAssignmentResponseDto>();
            CreateMap<CreateDeliveryAssignmentDto, DeliveryAssignment>();
            CreateMap<UpdateDeliveryAssignmentDto, DeliveryAssignment>();
        }
    }
}
