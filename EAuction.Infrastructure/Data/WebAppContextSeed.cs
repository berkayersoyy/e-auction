using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EAuction.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EAuction.Infrastructure.Data
{
    public class WebAppContextSeed
    {
        public static async Task SeedAsync(WebAppContext context,ILoggerFactory loggerFactory,int? retry=0)
        {
            int retryForAvailability = retry.Value;

            try
            {
                context.Database.Migrate();
                if (context.AppUsers.Any())
                {
                    context.AppUsers.AddRange(GetPreconfiguredOrders());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                if (retryForAvailability<50)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<WebAppContextSeed>();
                    log.LogError(exception.Message);
                    Thread.Sleep(2000);
                    await SeedAsync(context, loggerFactory, retryForAvailability);
                }
            }
        }

        private static IEnumerable<AppUser> GetPreconfiguredOrders()
        {
            return new List<AppUser>
            {
                new AppUser
                {
                    FirstName = "User FirstName",
                    LastName = "User LastName",
                    IsSeller = true,
                    IsBuyer = false
                }
            };
        }
    }
}