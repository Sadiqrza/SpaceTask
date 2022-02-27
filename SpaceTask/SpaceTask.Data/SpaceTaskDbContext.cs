using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpaceTask.Data.Entities;

namespace SpaceTask.Data
{
    public class SpaceTaskDbContext : DbContext
    {
        public IConfiguration Configuration { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }

        public SpaceTaskDbContext(DbContextOptions<SpaceTaskDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Watchlist>().HasKey(w => w.Id);
            modelBuilder.Entity<Watchlist>().HasIndex(w => new { w.UserId, w.MovieName }).IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
