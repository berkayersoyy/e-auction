
using System.Collections.Generic;
using System.Threading.Tasks;
using EAuction.Order.Domain.Repositories.Base;
namespace EAuction.Order.Domain.Repositories
{
    public interface IOrderRepository:IRepository<Entities.Order>
    {
        Task<IEnumerable<Entities.Order>> GetOrdersBySellerUserName(string userName);
    }
}