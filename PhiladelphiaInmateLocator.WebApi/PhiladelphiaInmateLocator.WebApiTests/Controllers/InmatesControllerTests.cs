namespace PhiladelphiaInmateLocator.WebApi.Controllers.Tests
{
    using Xunit;
    using PhiladelphiaInmateLocator.WebApi.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using PhiladelphiaInmateLocator.WebApi.Services.Interface;
    using Moq;
    using System.Threading.Tasks;
    using PhiladelphiaInmateLocator.WebApi.Models;
    using Microsoft.AspNetCore.Mvc;

    public class InmatesControllerTests
    {
        public InmatesControllerTests()
        {
        }

        private readonly IInmateService inmateService;

        public InmatesController CreateInmateController(IInmateService inmateService = null)
        {
            return new InmatesController(inmateService);
        }

        [Fact]
        public async Task GetInmatesForMyLocationTestAsync()
        {
            Mock<IInmateService> inmateService = new Mock<IInmateService>();

            InmatesController controller = this.CreateInmateController(inmateService.Object);
            ActionResult<List<Inmate>> inmates = await controller.GetInmatesForMyLocation().ConfigureAwait(false);
            Assert.True(false, "This test needs an implementation");
        }
    }
}