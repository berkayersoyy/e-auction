using AutoMapper;
using EAuction.Order.Application.Commands.OrderCreate;
using EventBusRabbitMQ.Events;

namespace EAuction.Order.WebApi.Mapping
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderCreateEvent, OrderCreateCommand>().ReverseMap();
        }
    }
}