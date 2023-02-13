using Microsoft.EntityFrameworkCore;

namespace GSMBulk.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<SMS> SMSDb { get; set; }
        public DbSet<Number> NumberDb { get; set; }

    }
}
