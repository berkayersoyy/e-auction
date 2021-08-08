using System.Collections.Generic;
using System.Threading.Tasks;
using EAuction.Sourcing.Api.Data.Abstractions;
using EAuction.Sourcing.Api.Entities;
using EAuction.Sourcing.Api.Repositories.Abstractions;
using MongoDB.Driver;

namespace EAuction.Sourcing.Api.Repositories
{
    public class AuctionRepository:IAuctionRepository
    {
        private readonly ISourcingContext _context;

        public AuctionRepository(ISourcingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Auction>> GetAuctions()
        {
            var auctions = await _context.Auctions.Find(p => true).ToListAsync();
            return auctions;
        }

        public async Task<Auction> GetAuction(string id)
        {
            var filter = await _context.Auctions.Find(a => a.Id == id).FirstOrDefaultAsync();
            return filter;
        }

        public async Task<Auction> GetAuctionByName(string name)
        {
            var filter = Builders<Auction>.Filter.Eq(m => m.Name, name);
            var result = await _context.Auctions.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task Create(Auction auction)
        {
            await _context.Auctions.InsertOneAsync(auction);
        }

        public async Task<bool> Update(Auction auction)
        {
            var updateResult = await _context.Auctions.ReplaceOneAsync(m => m.Id.Equals(auction.Id), replacement: auction);
            var result = updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            return result;
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Auction>.Filter.Eq(m => m.Id, id);
            DeleteResult result = await _context.Auctions.DeleteOneAsync(filter);
            var deleteResult = result.IsAcknowledged && result.DeletedCount > 0;
            return deleteResult;
        }
    }
}