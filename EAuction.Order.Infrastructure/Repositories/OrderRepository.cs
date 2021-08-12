using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAuction.Order.Domain.Repositories;
using EAuction.Order.Infrastructure.Data;
using EAuction.Order.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace EAuction.Order.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Domain.Entities.Order>,IOrderRepository
    {

        public OrderRepository(OrderContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Domain.Entities.Order>> GetOrdersBySellerUserName(string userName)
        {
            var orderList = await _context.Orders.Where(o => o.SellerUserName == userName).ToListAsync();
            return orderList;
        }
    }
}