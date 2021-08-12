using System.Collections.Generic;
using EAuction.Order.Application.Responses;
using MediatR;

namespace EAuction.Order.Application.Queries
{
    public class GetOrdersBySellerUserNameQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public GetOrdersBySellerUserNameQuery(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }

    }
}