using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Models.CarMarket
{
    public class CarMarketContext : IdentityDbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarHistory> CarHistories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<CarPhotoLink> CarPhotoLinks { get; set; }

        public CarMarketContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
