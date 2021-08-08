using System.Collections.Generic;
using System.Threading.Tasks;
using EAuction.Sourcing.Api.Entities;

namespace EAuction.Sourcing.Api.Repositories.Abstractions
{
    public interface IBidRepository
    {
        Task SendBid(Bid bid);
        Task<IEnumerable<Bid>> GetBidsByAuctionId(string id);
        Task<Bid> GetWinnerBid(string id);
    }
}