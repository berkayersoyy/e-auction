using EAuction.Sourcing.Api.Data.Abstractions;
using EAuction.Sourcing.Api.Entities;
using EAuction.Sourcing.Api.Settings;
using MongoDB.Driver;

namespace EAuction.Sourcing.Api.Data
{
    public class SourcingContext:ISourcingContext
    {
        public SourcingContext(ISourcingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Auctions = database.GetCollection<Auction>(nameof(Auction));
            Bids = database.GetCollection<Bid>(nameof(Bid));

            SourcingContextSeed.SeedData(Auctions);
        }
        public IMongoCollection<Auction> Auctions { get; }
        public IMongoCollection<Bid> Bids { get; }
    }
}