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

    public class UserService : IUserService
    {
        public InmateDatabase _inmateDatabase;

        public UserService(InmateDatabase inmateDatabase)
        {
            _inmateDatabase = inmateDatabase;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await this._inmateDatabase.Users.ToListAsync().ConfigureAwait(false);
        }
    }
}
