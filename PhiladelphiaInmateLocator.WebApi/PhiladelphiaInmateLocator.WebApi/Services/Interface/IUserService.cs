namespace PhiladelphiaInmateLocator.WebApi.Services.Interface
{
    using PhiladelphiaInmateLocator.WebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IUserService
    { 
        Task<List<User>> GetAllUsers();

        Task<User> Authenticate(string username, string password);

        Task<ClaimsIdentity> CreateClaimsIdentities(User user);
    }
}
