using Ferrum.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ferrum.Gateway.Data
{
    public class GatewayDbContext : DbContext
    {
        public GatewayDbContext()
        { }
        
        public GatewayDbContext(DbContextOptions<GatewayDbContext> options)
            :base(options) 
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientLogin> ClientLogins { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
