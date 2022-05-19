using Microsoft.EntityFrameworkCore;
using Task_bArt.Data.Entity;
using Task_bArt.Data.EntityConfigurations;

namespace Task_bArt.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Incident>? Incidents { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Contact>? Contacts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new IncidentConfiguration());
            builder.ApplyConfiguration(new ContactConfiguration());
        }
    }
}
