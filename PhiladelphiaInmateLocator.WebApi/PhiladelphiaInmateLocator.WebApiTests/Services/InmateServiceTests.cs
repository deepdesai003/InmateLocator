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
            inmateDatabase ??= new Mock<InmateDatabase>().Object;
            return new InmateService(inmateDatabase);
        }

        public static IEnumerable<object[]> mockForSerachbyName =>
                    new[]
                    {
                        new object[] { "Test1", "Test1",  new DateTime(1970, 05, 01) , 10 },
                        new object[] { "test3", "test3", new DateTime(1971, 12, 31), 30 },

                        //Case Insensitive
                        new object[] { "test1", "test1",  new DateTime(1970, 05, 01) , 10 },
                        new object[] { "TEST3", "TEST3", new DateTime(1971, 12, 31), 30 },

                        //empty first name returns a null object, test return ID as 0 for null object.
                        new object[] { string.Empty, "TEST3", new DateTime(1971, 12, 31), 0 },

                        //null returns a null object, test return ID as 0 for null object.
                        new object[] { null, "TEST3", new DateTime(1971, 12, 31), 0},
                    };

        #region"GetInmateByIDTest"
        [Theory]
        [InlineData(40, "TestInmate")]
        [InlineData(10, "Test1")]
        public async Task GetInmateByIDTest(int inputValue, string expectedValue)
        {
            InmateDatabase inmateDatabaseService = await this.GetInmateDatabase().ConfigureAwait(false);
            IInmateService inmateService = this.GetInmateService(inmateDatabaseService);

            Inmate inmate = await inmateService.GetInmateByID(inputValue).ConfigureAwait(false);
            Assert.Equal(expectedValue, inmate.FirstName);
        }
        #endregion"GetInmateByIDTest"


        #region"GetInmateByNameAndBirthDateTests"
        [Theory]
        [MemberData(nameof(mockForSerachbyName))]
        public async Task GetInmateByNameAndBirthDateTest(string inputFirstName, string inputLastName, DateTime inputDateOfBirth, int expectedReturnID)
        {
            InmateDatabase inmateDatabaseService = await this.GetInmateDatabase().ConfigureAwait(false);
            IInmateService inmateService = this.GetInmateService(inmateDatabaseService);

            Inmate inmate = await inmateService.GetInmateByNameAndBirthDate(inputFirstName, inputLastName, inputDateOfBirth).ConfigureAwait(false);
            Assert.Equal(expectedReturnID, inmate?.Id ?? 0);
        }
        #endregion"GetInmateByNameAndBirthDateTests"

        #region"GetAllInmatesTests"
        [Fact]
        public async Task GetAllInmatesTest()
        {
            InmateDatabase inmateDatabaseService = await this.GetInmateDatabase().ConfigureAwait(false);
            IInmateService inmateService = this.GetInmateService(inmateDatabaseService);

            List<Inmate> inmates = await inmateService.GetAllInmates().ConfigureAwait(false);
            Assert.Equal(4, inmates.Count);
        }

        [Fact]
        public async Task GetAllInmates_ReturnsEmptyTest()
        {
            InmateDatabase inmateDatabaseService = await this.GetInmateDatabase(true).ConfigureAwait(false);
            IInmateService inmateService = this.GetInmateService(inmateDatabaseService);

            List<Inmate> inmates = await inmateService.GetAllInmates().ConfigureAwait(false);
            Assert.Empty(inmates);
        }
        #endregion"GetAllInmatesTests"


        #region"GetInmatesForMyLocationTests"
        [Theory]
        [InlineData("Location1", 3)]
        [InlineData("Test Jail", 1)]
        public async Task GetInmatesForMyLocationTest(string inputLocation, int expectedCount)
        {
            InmateDatabase inmateDatabaseService = await this.GetInmateDatabase().ConfigureAwait(false);
            IInmateService inmateService = this.GetInmateService(inmateDatabaseService);

            List<Inmate> inmates = await inmateService.GetInmatesByLocation(inputLocation).ConfigureAwait(false);
            Assert.Equal(expectedCount, inmates.Count);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task GetInmatesForMyLocation_ReturnsEmptyTest(string inputLocation)
        {
            InmateDatabase inmateDatabaseService = await this.GetInmateDatabase(true).ConfigureAwait(false);
            IInmateService inmateService = this.GetInmateService(inmateDatabaseService);

            List<Inmate> inmates = await inmateService.GetInmatesByLocation(inputLocation).ConfigureAwait(false);
            Assert.Empty(inmates);
        }
        
        #endregion"GetInmatesForMyLocationTests"

        #region"Helper"
        internal async Task<InmateDatabase> GetInmateDatabase(bool onlyClearData = false)
        {
            var options = new DbContextOptionsBuilder<InmateDatabase>()
                .UseInMemoryDatabase(databaseName: "Inmate")
                .Options;

            var context = new InmateDatabase(options);

            context.Inmates.RemoveRange(context.Inmates);

            if(onlyClearData)
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
                return context;
            }

            List<Inmate> inmates = new List<Inmate>
            {
                new Inmate { Id = 10, FirstName = "Test1", LastName = "Test1", DateOfBirth = new DateTime(1970, 05, 01), Location = "Location1" },
                new Inmate { Id = 20, FirstName = "Test2", LastName = "Test2", DateOfBirth = new DateTime(1975, 10, 10), Location = "Location1" },
                new Inmate { Id = 30, FirstName = "Test3", LastName = "Test3", DateOfBirth = new DateTime(1971, 12, 31), Location = "Location1" },
                new Inmate { Id = 40, FirstName = "TestInmate", LastName = "TestLastName", Location = "Test Jail" }
            };

            context.Inmates.AddRange(inmates);
            await context.SaveChangesAsync().ConfigureAwait(false);
            
            return context;
            #endregion"Helper"
        }
    }
}
