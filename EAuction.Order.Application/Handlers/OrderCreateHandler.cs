using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EAuction.Order.Application.Commands.OrderCreate;
using EAuction.Order.Application.Responses;
using EAuction.Order.Domain.Repositories;
using MediatR;

namespace EAuction.Order.Application.Handlers
{
    public class OrderCreateHandler : IRequestHandler<OrderCreateCommand,OrderResponse>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderCreateHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Domain.Entities.Order>(request);
            if (orderEntity==null)
            {
                throw new ApplicationException("Entity could not be mapped!");
            }

            var order = await _orderRepository.AddAsync(orderEntity);

            var orderResponse = _mapper.Map<OrderResponse>(order);

            return orderResponse;
        }
    }
}