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
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class InmatesControllerTests
    {
        public InmatesControllerTests()
        {
        }
    
        public InmatesController CreateInmateController(Mock<HttpContext> httpContext, IInmateService inmateService = null)
        {
            var controller = new InmatesController(inmateService);
            controller.ControllerContext.HttpContext = httpContext.Object;
            return controller;
        }

        #region"GetInmateByID"
        [Theory]
        [InlineData(10,"Test10")]
        [InlineData(20, "Test20")]
        public async Task GetInmateByID(int inputValue, string expectedValue)
        {
            //Mock Service Response.
            Mock<IInmateService> inmateService = this.GetMockGetInmateByID(inputValue);
            //Create controller with mocked service.
            InmatesController controllerResponse = this.CreateInmateController(new Mock<HttpContext>(), inmateService.Object);

            //Call the Api/Controller
            ActionResult<Inmate> result = await controllerResponse.GetInmateByID(inputValue).ConfigureAwait(false);
            
            //Verify the object response type
            ActionResult<Inmate> viewResult = Assert.IsType<ActionResult<Inmate>>(result);

            //Verify the number of results are right
            Assert.Equal(expectedValue, viewResult.Value.LastName);
        }

        #endregion"GetInmateByID"

        #region"GetAllInmates"
        ///<summary>
        ///Check if GetAllInmatesTest return correct data type. 
        ///</summary>
        [Fact]
        public async Task GetAllInmatesTest()
        {
            //Mock Service Response.
            Mock<IInmateService> inmateService = this.GetMockGetAllInmates();

            //Create controller with mocked service.
            InmatesController controllerResponse = this.CreateInmateController(new Mock<HttpContext>(), inmateService.Object);

            //Call the Api/Controller
            ActionResult<List<Inmate>> result = await controllerResponse.GetAllInmates().ConfigureAwait(false);

            //Verify the object response type
            ActionResult<List<Inmate>> viewResult = Assert.IsType<ActionResult<List<Inmate>>>(result);

            //Verify the number of results are right
            Assert.Equal(3, viewResult.Value.Count);
        }
        #endregion"GetAllInmates"


        #region "GetInmatesForMyLocationTests"
        ///<summary>
        ///Check if a token with a location returns a response. 
        ///</summary>
        [Fact]
        public async Task GetInmatesForMyLocationValidLocationTest()
        {
            //Mock Http
            var httpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Role", "warden"),
                new Claim("location", "Location1"),
            }, "mock"));

            httpContext.Setup(x => x.User).Returns(user);

            //Mock Service Response.
            Mock<IInmateService> inmateService = this.GetMockGetInmatesForMyLocation();

            //Create controller with mocked service.
            InmatesController controllerResponse = this.CreateInmateController(httpContext, inmateService.Object);

            ActionResult<List<Inmate>> result = await controllerResponse.GetInmatesForMyLocation().ConfigureAwait(false);

            //Verify the object response type
            ActionResult<List<Inmate>> viewResult = Assert.IsType<ActionResult<List<Inmate>>>(result);

            //Verify the number of results are right
            Assert.Equal(3, viewResult.Value.Count);
        }

        ///<summary>
        ///Check if a token without a location returns a 404.
        ///</summary>
        ///<returns></returns>
        [Fact]
        public async Task GetInmatesForMyLocationInvalidLocationTest()
        {
            //Mock Http
            var httpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Role", "Adminstrator"),
            }, "mock"));

            httpContext.Setup(x => x.User).Returns(user);

            //Mock Service Response.
            Mock<IInmateService> inmateService = this.GetMockGetInmatesForMyLocation();

            //Create controller with mocked service.
            InmatesController controllerResponse = this.CreateInmateController(httpContext, inmateService.Object);


            var result = await controllerResponse.GetInmatesForMyLocation().ConfigureAwait(false);
            Assert.Equal(404, ((ObjectResult)result.Result).StatusCode);
        }
        #endregion "GetInmatesForMyLocationTests"

        #region "Helper"
        ///<summary>
        ///Get Mock Data
        ///</summary>
        ///<returns>Mock responst of GetInmatesForMyLocation</returns>
        internal Mock<IInmateService> GetMockGetInmateByID(int inputValue)
        {
            Mock<IInmateService> getMockGetInmateByID = new Mock<IInmateService>();
            Inmate inmate10 = new Inmate { Id = 10, FirstName = "Test10", LastName = "Test10", Location = "Location10" };
            Inmate inmate20 = new Inmate { Id = 10, FirstName = "Test20", LastName = "Test20", Location = "Location12" };

            //Mock GetInmatesForMyLocation() and its response
            getMockGetInmateByID.Setup(service => service.GetInmateByID(10)).Returns(Task.FromResult(inmate10));
            getMockGetInmateByID.Setup(service => service.GetInmateByID(20)).Returns(Task.FromResult(inmate20));
            return getMockGetInmateByID;
        }

        ///<summary>
        ///Get Mock Data
        ///</summary>
        ///<returns>Mock responst of GetInmatesForMyLocation</returns>
        internal Mock<IInmateService> GetMockGetInmatesForMyLocation()
        {
            Mock<IInmateService> getInmatesForMyLocation = new Mock<IInmateService>();
            List<Inmate> inmatesbyLocation = new List<Inmate>
            {
                new Inmate{ Id = 10, FirstName = "Test1", LastName = "Test1", Location = "Location1" },
                new Inmate{ Id = 20, FirstName = "Test2", LastName = "Test2", Location = "Location1" },
                new Inmate{ Id = 30, FirstName = "Test3", LastName = "Test3", Location = "Location1" },
            };

            //Mock GetInmatesForMyLocation() and its response
            getInmatesForMyLocation.Setup(service => service.GetInmatesForMyLocation(It.IsAny<string>())).Returns(Task.FromResult(inmatesbyLocation));
            return getInmatesForMyLocation;
        }

        ///<summary>
        ///Get Mock Data
        ///</summary>
        ///<returns>Mock responst of GetMockGetAllInmates</returns>
        internal Mock<IInmateService> GetMockGetAllInmates()
        {
            Mock<IInmateService> getAllInmates = new Mock<IInmateService>();
            List<Inmate> inmates = new List<Inmate>
            {
                new Inmate{ Id = 10, FirstName = "Test1", LastName = "Test1", Location = "Location1" },
                new Inmate{ Id = 20, FirstName = "Test2", LastName = "Test2", Location = "Location1" },
                new Inmate{ Id = 30, FirstName = "Test3", LastName = "Test3", Location = "Location1" },
            };

            //Mock GetAllInmates() and its response
            getAllInmates.Setup(service => service.GetAllInmates()).Returns(Task.FromResult(inmates));
            return getAllInmates;
        }
        #endregion "Helper"

    }
}