namespace PhiladelphiaInmateLocator.WebApi.Services.Interface
{
    using Microsoft.EntityFrameworkCore;
    using PhiladelphiaInmateLocator.WebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IInmateService
    {
        Task<List<Inmate>> SetData ();

        Task<Inmate> GetInmateByID (int id);

        Task<List<Inmate>> GetInmateByNameAndBirthDate (string firstName, string lastName, DateTime dateOfBirth);

        Task<List<Inmate>> GetAllInmates ();

        List<Inmate> GetInmatesForMyLocation (string Location);
    }
}
