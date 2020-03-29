namespace PhiladelphiaInmateLocator.WebApi.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InmateContext :DbContext
    {
        public InmateContext(DbContextOptions<InmateContext> options):base(options)
        {
        }

        public DbSet<Inmate> Inmates { get; set; }

        public DbSet<Location> Location { get; set; }
    }
}
