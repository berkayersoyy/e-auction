using EAuction.Order.Domain.Repositories;
using EAuction.Order.Domain.Repositories.Base;
using EAuction.Order.Infrastructure.Data;
using EAuction.Order.Infrastructure.Repositories;
using EAuction.Order.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EAuction.Order.Infrastructure.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            //services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
            //    ServiceLifetime.Singleton, ServiceLifetime.Singleton);

            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("OrderConnection"),
                    b => b.MigrationsAssembly(typeof(OrderContext).Assembly.FullName)), ServiceLifetime.Singleton);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IOrderRepository, OrderRepository>();


            return services;
        }
    }
}