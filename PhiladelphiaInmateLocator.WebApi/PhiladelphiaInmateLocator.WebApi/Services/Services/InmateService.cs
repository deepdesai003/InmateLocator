namespace PhiladelphiaInmateLocator.WebApi.Services.Services
{
    using Microsoft.EntityFrameworkCore;
    using PhiladelphiaInmateLocator.WebApi.Entities;
    using PhiladelphiaInmateLocator.WebApi.Models;
    using PhiladelphiaInmateLocator.WebApi.Services.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public class InmateService : IInmateService
    {
        public InmateDatabase _inmateDatabase;
        public InmateService(InmateDatabase inmateDatabase)
        {
            _inmateDatabase = inmateDatabase;
        }
        public async Task ClearData()
        {
            _inmateDatabase.Inmates.RemoveRange(_inmateDatabase.Inmates);
            await this._inmateDatabase.SaveChangesAsync();
        }

        public async Task SetInmates()
        {
            _inmateDatabase.Inmates.Add(new Inmate { Id = 420, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1989, 01, 01), Location = "City Hall" });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 421, FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1971, 05, 12), Location = "City Hall" });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 422, FirstName = "Julia", LastName = "Roberts", DateOfBirth = new DateTime(1972, 06, 10), Location = "City Hall" });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 423, FirstName = "Bill", LastName = "Cosby", DateOfBirth = new DateTime(1965, 10, 10), Location = "South Prison" });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 424, FirstName = "Matthew", LastName = "Wade", DateOfBirth = new DateTime(1993, 12, 20), Location = "Alcatraz" });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 425, FirstName = "Michael", LastName = "Clarke", DateOfBirth = new DateTime(1988, 11, 20), Location = "Alcatraz" });
            _inmateDatabase.Inmates.Add(new Inmate { Id = 426, FirstName = "Jack", LastName = "Reacher", DateOfBirth = new DateTime(1991, 12, 30), Location = "Alcatraz" });
            await this._inmateDatabase.SaveChangesAsync();
        }

        public async Task<List<Inmate>> SetData()
        {
            await this.ClearData().ConfigureAwait(false);
            await this.SetInmates().ConfigureAwait(false);
            return await _inmateDatabase.Inmates.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Inmate> GetInmateByID(int id)
        {
            return await _inmateDatabase.Inmates.FindAsync(id);
        }

        public async Task<Inmate> GetInmateByNameAndBirthDate(string firstName, string lastName, DateTime dateOfBirth)
        {
            return await _inmateDatabase.Inmates.FirstOrDefaultAsync(inmate =>
            inmate.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase)
                    && inmate.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)
                    && inmate.DateOfBirth.Equals(dateOfBirth));
        }

        public async Task<List<Inmate>> GetAllInmates()
        {
            return await _inmateDatabase.Inmates.ToListAsync();
        }

        public async Task<List<Inmate>> GetInmatesByLocation(string Location)
        {
            List<Inmate> inmates = await _inmateDatabase.Inmates.ToListAsync().ConfigureAwait(false);
            return inmates.Where(inmate => inmate.Location.Equals(Location)).ToList();
        }

        public async Task AddInmate(Inmate inmate)
        {
            _inmateDatabase.Inmates.Add(inmate);
            await this._inmateDatabase.SaveChangesAsync().ConfigureAwait(false); ;
        }
    }
}
