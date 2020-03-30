namespace PhiladelphiaInmateLocator.WebApi.Services.Services
{
    using Microsoft.EntityFrameworkCore;
    using PhiladelphiaInmateLocator.WebApi.Models;
    using PhiladelphiaInmateLocator.WebApi.Services.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InmateDatabaseService : DbContext
    {
        public InmateDatabaseService (DbContextOptions<InmateDatabaseService> inmateDatabase) : base(inmateDatabase)
        {
        }

        public DbSet<Inmate> Inmates { get; set; }

        public DbSet<Location> Location { get; set; }
    }
}