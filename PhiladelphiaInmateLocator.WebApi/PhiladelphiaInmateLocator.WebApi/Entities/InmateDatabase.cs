using Microsoft.EntityFrameworkCore;
    using PhiladelphiaInmateLocator.WebApi.Models;


amespacee PhiladelphiaInmateLocator.WebApi.Entities
{
    
    public class InmateDatabase : DbContext
    {
        public InmateDatabase(DbContextOptions<InmateDatabase> options) : base(options)
        {
        }

        public DbSet<Inmate> Inmates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inmate>().ToTable("INMATES");
        }
    }
}
