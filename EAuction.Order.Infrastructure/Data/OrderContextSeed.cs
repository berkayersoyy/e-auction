using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAuction.Order.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Domain.Entities.Order> GetPreconfiguredOrders()
        {
            return new List<Domain.Entities.Order>()
            {
                new Domain.Entities.Order()
                {
                    AuctionId = Guid.NewGuid().ToString(),
                    ProductId = Guid.NewGuid().ToString(),
                    SellerUserName = "test@test.com",
                    UnitPrice = 10,
                    TotalPrice = 1000,
                    CreatedAt = DateTime.UtcNow
                },
                new Domain.Entities.Order()
                {
                    AuctionId = Guid.NewGuid().ToString(),
                    ProductId = Guid.NewGuid().ToString(),
                    SellerUserName = "test1@test.com",
                    UnitPrice = 10,
                    TotalPrice = 1000,
                    CreatedAt = DateTime.UtcNow
                },
                new Domain.Entities.Order()
                {
                    AuctionId = Guid.NewGuid().ToString(),
                    ProductId = Guid.NewGuid().ToString(),
                    SellerUserName = "test2@test.com",
                    UnitPrice = 10,
                    TotalPrice = 1000,
                    CreatedAt = DateTime.UtcNow
                }
            };
        }
    }
}