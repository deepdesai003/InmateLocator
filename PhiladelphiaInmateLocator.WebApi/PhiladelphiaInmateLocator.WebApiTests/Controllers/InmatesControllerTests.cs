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
    using System.Net;
    using Microsoft.AspNetCore.TestHost;
    using System.Net.Http;
    using Microsoft.AspNetCore.Hosting;
    using System.Linq;

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

        #region"GetInmateByIDTests"
        ///<summary>
        ///Check GetInmateByID returns a response. 
        ///</summary>
        [Fact]
        public async Task GetInmateByIDTest()
        {
            //Mock Service Response.
            Mock<IInmateService> inmateService = this.GetMockInmateService();
            //Create controller with mocked service.
            InmatesController controllerResponse = this.CreateInmateController(new Mock<HttpContext>(), inmateService.Object);

            //Call the Api/Controller
            ActionResult<Inmate> result = await controllerResponse.GetInmateByID(10).ConfigureAwait(false);
            
            //Verify the object response type
            ActionResult<Inmate> viewResult = Assert.IsType<ActionResult<Inmate>>(result);

            //Verify the number of results are right
            Assert.Equal(10, viewResult.Value.Id);
        }

        ///<summary>
        ///Check GetInmateByID returns a 404 when not inmate is not found. 
        ///</summary>
        [Fact]
        public async Task GetInmateByIDTest_Returns404()
        {
            //Mock Service Response.
            Mock<IInmateService> inmateService = this.GetMockInmateService();
            //Create controller with mocked service.
            InmatesController controllerResponse = this.CreateInmateController(new Mock<HttpContext>(), inmateService.Object);

            //Call the Api/Controller
            ActionResult<Inmate> result = await controllerResponse.GetInmateByID(10).ConfigureAwait(false);

            //Verify the object response type
            Assert.IsType<ActionResult<Inmate>>(result);

            //Verify the number of results are right
            Assert.Equal(404, ((ObjectResult)result.Result).StatusCode);
        }
        #endregion"GetInmateByIDTests"

        #region"GetInmateByNameAndBirthDateTests"
        /// <summary>
        /// Check GetInmateByNameAndBirthDate returns a response.
        /// </summary>
        [Fact]
        public async Task GetInmateByNameAndBirthDateTest()
        {
            //Mock Service Response.
            Mock<IInmateService> inmateService = this.GetMockInmateService();
            //Create controller with mocked service.
            InmatesController controllerResponse = this.CreateInmateController(new Mock<HttpContext>(), inmateService.Object);

            //Call the Api/Controller
            ActionResult<Inmate> result = await controllerResponse.GetInmateByNameAndBirthDate("Test1", "Test1", new DateTime(1970, 05, 01)).ConfigureAwait(false);

            //Verify the object response type
            ActionResult<Inmate> viewResult = Assert.IsType<ActionResult<Inmate>>(result);

            //Verify the number of results are right
            Assert.Equal("Test1", viewResult.Value.FirstName);
        }

        /// <summary>
        /// Check GetInmateByNameAndBirthDate returns a 404 when not inmate is not found. 
        /// </summary>
        [Fact]
        public async Task GetInmateByNameAndBirthDateTest_Returns404()
        {
            //Mock Service Response.
            Mock<IInmateService> inmateService = this.GetMockInmateService();
            //Create controller with mocked service.
            InmatesController controllerResponse = this.CreateInmateController(new Mock<HttpContext>(), inmateService.Object);

            
            //Call the Api/Controller
            ActionResult<Inmate> result = await controllerResponse.GetInmateByNameAndBirthDate("Test2", "Test1", new DateTime(1970, 05, 01)).ConfigureAwait(false);
         
            //Verify the object response type
            Assert.IsType<ActionResult<Inmate>>(result);

            //Verify the number of results are right
            Assert.Equal(404, ((ObjectResult)result.Result).StatusCode);
        }
        #endregion"GetInmateByNameAndBirthDateTests"


        #region"GetAllInmatesTests"
        ///<summary>
        ///Check if GetAllInmatesTest return correct data type. 
        ///</summary>
        [Fact]
        public async Task GetAllInmatesTest()
        {
            //Mock Service Response.
            Mock<IInmateService> inmateService = this.GetMockInmateService();

            //Create controller with mocked service.
            InmatesController controllerResponse = this.CreateInmateController(new Mock<HttpContext>(), inmateService.Object);

            //Call the Api/Controller
            ActionResult<List<Inmate>> result = await controllerResponse.GetAllInmates().ConfigureAwait(false);

            //Verify the object response type
            ActionResult<List<Inmate>> viewResult = Assert.IsType<ActionResult<List<Inmate>>>(result);

            //Verify the number of results are right
            Assert.Equal(3, viewResult.Value.Count);
        }
        #endregion"GetAllInmatesTests"


        #region "GetInmatesForMyLocationTests"
        ///<summary>
        ///Check if a token with a location returns a response. 
        ///</summary>
        [Fact]
        public async Task GetInmatesForMyLocation_ValidLocationTest()
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
            Mock<IInmateService> inmateService = this.GetMockInmateService();

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
        public async Task GetInmatesForMyLocation_InvalidLocationTest()
        {
            //Mock Http
            var httpContext = new Mock<HttpContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Role", "Adminstrator"),
            }, "mock"));

            httpContext.Setup(x => x.User).Returns(user);

            //Mock Service Response.
            Mock<IInmateService> inmateService = this.GetMockInmateService();

            //Create controller with mocked service.
            InmatesController controllerResponse = this.CreateInmateController(httpContext, inmateService.Object);
            
            //Call the Api
            var result = await controllerResponse.GetInmatesForMyLocation().ConfigureAwait(false);

            //Verify the number of results returns a 404.
            Assert.Equal(404, ((ObjectResult)result.Result).StatusCode);
        }
        #endregion "GetInmatesForMyLocationTests"

        #region "Helper"
        ///<summary>
        ///Get Mock Data
        ///</summary>
        ///<returns>Mock responst of GetInmatesForMyLocation</returns>
        internal Mock<IInmateService> GetMockInmateService()
        {
            Mock<IInmateService> mockInmateService = new Mock<IInmateService>();

            List<Inmate> inmates = new List<Inmate>
            {
                new Inmate{ Id = 10, FirstName = "Test1", LastName = "Test1", DateOfBirth = new DateTime(1970, 05, 01), Location = "Location1" },
                new Inmate{ Id = 20, FirstName = "Test2", LastName = "Test2", DateOfBirth = new DateTime(1975, 10, 10), Location = "Location1" },
                new Inmate{ Id = 30, FirstName = "Test3", LastName = "Test3", DateOfBirth = new DateTime(1971, 12, 31), Location = "Location1" },
            };

            //Mock GetInmateByID() and its response
            mockInmateService.Setup(service => service.GetInmateByID(10)).Returns(Task.FromResult(inmates.FirstOrDefault()));
            mockInmateService.Setup(service => service.GetInmateByID(20)).Returns(Task.FromResult(inmates.ElementAt(1)));

            //Mock GetInmateByID() and its response
            mockInmateService.Setup(service => service.GetInmateByNameAndBirthDate("Test1", "Test1", new DateTime(1970, 05, 01))).Returns(Task.FromResult(inmates.FirstOrDefault()));
            mockInmateService.Setup(service => service.GetInmateByNameAndBirthDate("Test2", "Test2", new DateTime(1975, 10, 10))).Returns(Task.FromResult(inmates.ElementAt(1)));


            //Mock GetAllInmates() and its response
            mockInmateService.Setup(service => service.GetAllInmates()).Returns(Task.FromResult(inmates));

            //Mock GetInmatesForMyLocation() and its response
            mockInmateService.Setup(service => service.GetInmatesByLocation("Location1")).Returns(Task.FromResult(inmates));

            return mockInmateService;
        }
        #endregion "Helper"

    }
}