using Microsoft.EntityFrameworkCore;
using RestroRouting.Data.Configurations;
using RestroRouting.Domain.Entities;

namespace RestroRouting.Data.Infrastructure
{
    public class RestroRoutingContext : DbContext
    {
        public RestroRoutingContext(DbContextOptions<RestroRoutingContext> options): base(options)
        {

        }

        public DbSet<Booth> Booths { get; set; }

        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new BoothEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
