using AutoMapper;
using EAuction.Order.Application.Commands.OrderCreate;
using EAuction.Order.Application.Responses;

namespace EAuction.Order.Application.Mapper
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Domain.Entities.Order, OrderCreateCommand>().ReverseMap();
            CreateMap<Domain.Entities.Order, OrderResponse>().ReverseMap();
        }
    }
}