namespace PhiladelphiaInmateLocator.WebApiTests.Services
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using PhiladelphiaInmateLocator.WebApi.Entities;
    using PhiladelphiaInmateLocator.WebApi.Models;
    using PhiladelphiaInmateLocator.WebApi.Services.Interface;
    using PhiladelphiaInmateLocator.WebApi.Services.Services;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Abstractions;

    public class InmateServiceTests
    {
        public InmateServiceTests()
        {
        }

        private IInmateService GetInmateService(InmateDatabase inmateDatabase = null)
        {
            return new InmateService(inmateDatabase);
        }

        [Theory]
        [InlineData(20, "TestInmate")]
        public async Task GetInmateByIDTest(int inputValue, string expectedValue)
        {
            InmateDatabase inmateDatabaseService = await this.GetInmateDatabase().ConfigureAwait(false);
            IInmateService inmateService = this.GetInmateService(inmateDatabaseService);
            
            Inmate inmate = await inmateService.GetInmateByID(inputValue).ConfigureAwait(false);
            Assert.Equal(expectedValue, inmate.FirstName);
        }

        internal async Task<InmateDatabase> GetInmateDatabase()
        {
            var options = new DbContextOptionsBuilder<InmateDatabase>()
                .UseInMemoryDatabase(databaseName: "Inmate")
                .Options;

            var context = new InmateDatabase(options);
            context.Inmates.Add(new Inmate { Id = 20, FirstName = "TestInmate", LastName = "TestLastName", Location = "Test Jail" });
            await context.SaveChangesAsync().ConfigureAwait(false);
            
            return context;
        }
    }
}
