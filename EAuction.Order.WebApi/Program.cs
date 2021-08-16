using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using EAuction.Order.WebApi.Extensions;

namespace EAuction.Order.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build()
                .MigrateDatabase().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseDefaultServiceProvider(opt => opt.ValidateScopes = false);
    }
}
