using EAuction.Sourcing.Api.Entities;
using MongoDB.Driver;

namespace EAuction.Sourcing.Api.Data.Abstractions
{
    public interface ISourcingContext
    {
        IMongoCollection<Auction> Auctions { get; }
        IMongoCollection<Bid> Bids { get; }

    }
}