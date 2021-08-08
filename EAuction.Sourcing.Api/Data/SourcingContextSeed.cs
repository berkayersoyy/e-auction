using System;
using System.Collections.Generic;
using EAuction.Sourcing.Api.Data.Abstractions;
using EAuction.Sourcing.Api.Entities;
using MongoDB.Driver;

namespace EAuction.Sourcing.Api.Data
{
    public class SourcingContextSeed
    {
        public static void SeedData(IMongoCollection<Auction> auctionCollection)
        {
            bool exist = auctionCollection.Find(p => true).Any();
            if (!exist)
            {
                auctionCollection.InsertManyAsync(ConfigureSeedData());
            }
        }

        private static IEnumerable<Auction> ConfigureSeedData()
        {
            return new List<Auction>
            {
                new Auction
                {
                    Name = "Auction 1",
                    Description = "Auction Desc 1",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(10),
                    IncludeSellers = new List<string>
                    {
                        "seller1@domain.com",
                        "seller2@domain.com",
                        "seller3@domain.com"
                    },
                    Quantity = 5,
                    Status = (int)Status.Active,
                    ProductId = "123456789123456789123456"
                },
                new Auction
                {
                    Name = "Auction 2",
                    Description = "Auction Desc 2",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(10),
                    IncludeSellers = new List<string>
                    {
                        "seller1@domain.com",
                        "seller2@domain.com",
                        "seller3@domain.com"
                    },
                    Quantity = 5,
                    Status = (int)Status.Active,
                    ProductId = "123456789123456789123456"
                },
                new Auction
                {
                    Name = "Auction 3",
                    Description = "Auction Desc 3",
                    CreatedAt = DateTime.Now,
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now.AddDays(10),
                    IncludeSellers = new List<string>
                    {
                        "seller1@domain.com",
                        "seller2@domain.com",
                        "seller3@domain.com"
                    },
                    Quantity = 5,
                    Status = (int)Status.Active,
                    ProductId = "123456789123456789123456"
                },
            };
        }
    }
}