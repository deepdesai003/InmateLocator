namespace PhiladelphiaInmateLocator.WebApi.Entities
{
    using Microsoft.EntityFrameworkCore;
    using PhiladelphiaInmateLocator.WebApi.Models;
    using PhiladelphiaInmateLocator.WebApi.Services.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InmateDatabase : DbContext
    {
        public InmateDatabase(DbContextOptions<InmateDatabase> inmateDatabase) : base(inmateDatabase)
        {
        }

        public DbSet<Inmate> Inmates { get; set; }
    }
}