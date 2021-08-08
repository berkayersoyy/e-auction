using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAuction.Sourcing.Api.Data.Abstractions;
using EAuction.Sourcing.Api.Entities;
using EAuction.Sourcing.Api.Repositories.Abstractions;
using MongoDB.Driver;

namespace EAuction.Sourcing.Api.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly ISourcingContext _context;

        public BidRepository(ISourcingContext context)
        {
            _context = context;
        }

        public async Task SendBid(Bid bid)
        {
            await _context.Bids.InsertOneAsync(bid);
        }

        public async Task<IEnumerable<Bid>> GetBidsByAuctionId(string id)
        {
            var filter = Builders<Bid>.Filter.Eq(a => a.AuctionId, id);
            var bids = await _context.Bids.Find(filter).ToListAsync();
            bids = bids.OrderByDescending(a => a.CreatedAt).GroupBy(a => a.SellerUserName).Select(a => new Bid
            {
                AuctionId = a.FirstOrDefault().AuctionId,
                Price = a.FirstOrDefault().Price,
                CreatedAt = a.FirstOrDefault().CreatedAt,
                SellerUserName = a.FirstOrDefault().SellerUserName,
                ProductId = a.FirstOrDefault().ProductId,
                Id = a.FirstOrDefault().Id
            }).ToList();
            return bids;
        }

        public async Task<Bid> GetWinnerBid(string id)
        {
            var bids = await GetBidsByAuctionId(id);
            var result = bids.OrderByDescending(a => a.Price).FirstOrDefault();
            return result;
        }
    }
}