namespace PhiladelphiaInmateLocator.WebApi.Services.Services
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using PhiladelphiaInmateLocator.WebApi.Models;
    using PhiladelphiaInmateLocator.WebApi.Services.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;


    public class InmateService : IInmateService
    {
        public InmateDatabaseService _inmateDatabase;
        public InmateService (InmateDatabaseService inmateDatabase)
        {
            _inmateDatabase = inmateDatabase;
        }

        public async Task<List<Inmate>> SetData ()
        {
            _inmateDatabase.Inmates.Add(new Inmate { Id = 420, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1989, 01, 01), LocationID = 1 });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 421, FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1971, 05, 12), LocationID = 1 });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 422, FirstName = "Julia", LastName = "Roberts", DateOfBirth = new DateTime(1972, 06, 10), LocationID = 1 });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 423, FirstName = "Bill", LastName = "Cosby", DateOfBirth = new DateTime(1965, 10, 10), LocationID = 2 });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 424, FirstName = "Matthew", LastName = "Wade", DateOfBirth = new DateTime(1993, 12, 20), LocationID = 3 });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 425, FirstName = "Michael", LastName = "Clarke", DateOfBirth = new DateTime(1988, 11, 20), LocationID = 3 });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 426, FirstName = "Jack", LastName = "Reacher", DateOfBirth = new DateTime(1991, 12, 30), LocationID = 3 });
            _inmateDatabase.Location.Add(new Location { Id = 1, Name = "City Hall" });
            _inmateDatabase.Location.Add(new Location { Id = 2, Name = "South Prison" });
            _inmateDatabase.Location.Add(new Location { Id = 3, Name = "Alcatraz" });
            await this._inmateDatabase.SaveChangesAsync();

            return await _inmateDatabase.Inmates.ToListAsync();
        }

        public async Task<Inmate> GetInmateByID (int id)
        {
            return await _inmateDatabase.Inmates.FindAsync(id);
        }

        public async Task<List<Inmate>> GetInmateByNameAndBirthDate (string firstName, string lastName, DateTime dateOfBirth)
        {
            return await _inmateDatabase.Inmates
                .Where(inmate => inmate.FirstName.Equals(firstName) && inmate.LastName.Equals(lastName) && inmate.DateOfBirth.Equals(dateOfBirth)).ToListAsync();
        }

        public async Task<List<Inmate>> GetAllInmates ()
        {
            return await _inmateDatabase.Inmates.ToListAsync();
        }

        public List<Inmate> GetInmatesForMyLocation (string Location)
        {
            return _inmateDatabase.Location
                .Where(loc => loc.Name.Equals(Location))
                .Join(_inmateDatabase.Inmates,
                loc => new { Id = loc.Id },
                inmate => new { Id = inmate.LocationID },
                (loc, inmate) => inmate).ToList();
        }
    }
}
