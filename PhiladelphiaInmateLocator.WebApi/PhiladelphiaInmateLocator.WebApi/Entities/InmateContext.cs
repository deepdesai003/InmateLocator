namespace PhiladelphiaInmateLocator.WebApi.Entities
{
    using Microsoft.EntityFrameworkCore;
    using PhiladelphiaInmateLocator.WebApi.Models;

    public class InmateContext : DbContext
    {
        public InmateContext (DbContextOptions<InmateContext> options) : base(options)
        {
        }

        public DbSet<Inmate> Inmates { get; set; }
    }
}
