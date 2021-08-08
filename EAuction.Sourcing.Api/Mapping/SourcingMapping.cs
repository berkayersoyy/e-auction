using AutoMapper;
using EAuction.Sourcing.Api.Entities;
using EventBusRabbitMQ.Events;

namespace EAuction.Sourcing.Api.Mapping
{
    public class SourcingMapping : Profile
    {
        public SourcingMapping()
        {
            CreateMap<OrderCreateEvent, Bid>().ReverseMap();
        }
    }
}