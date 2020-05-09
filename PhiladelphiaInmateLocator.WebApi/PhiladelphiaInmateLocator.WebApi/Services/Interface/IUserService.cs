namespace PhiladelphiaInmateLocator.WebApi.Services.Interface
{
    using PhiladelphiaInmateLocator.WebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IUserService
    { 
        Task<List<User>> GetAllUsers();
    }
}
