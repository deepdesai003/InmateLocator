namespace PhiladelphiaInmateLocator.WebApi.Entities
{
    using Microsoft.EntityFrameworkCore;
    using PhiladelphiaInmateLocator.WebApi.Models;

    public class InmateDatabase : DbContext
    {
        public InmateDatabase(DbContextOptions<InmateDatabase> options) : base(options)
        {
        }

        public DbSet<Inmate> Inmates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inmate>().ToTable("Inmates");
        }
    }
}