using Microsoft.EntityFrameworkCore;

namespace EAuction.Order.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options):base(options)
        {
            
        }

        public DbSet<Domain.Entities.Order> Orders { get; set; }
    }
}