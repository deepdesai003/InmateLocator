namespace PhiladelphiaInmateLocator.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using PhiladelphiaInmateLocator.WebApi.Models;
    using PhiladelphiaInmateLocator.WebApi.Services.Interface;

    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Authorize]
    public class InmatesController : Controller
    {
        private readonly IInmateService _inmatesService;

        public InmatesController(IInmateService inmatesService)
        {
            this._inmatesService = inmatesService;
        }

        ///<summary>
        ///Set the Data for Inmates and location in the memory.
        ///</summary>
        ///<remarks>
        ///Creates a list of Inmates and Location.
        ///
        ///Run this first to set the Data for test other Api Calls.
        ///</remarks>
        ///<returns>A List of newly created inmates </returns>
        [HttpPost("SetData")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Inmate>>> SetData()
        {
            return await this._inmatesService.SetData();
        }

        /// <summary>
        /// Get the Inmate for this ID.
        /// </summary>
        /// <remarks>
        /// Sample Request: api/Inmates/5
        /// 
        /// Requires: Non-authenticated users 
        ///</remarks>
        /// <response code="404">Inmate not found</response>  
        /// <returns>Inmate Object</returns>
        [HttpGet("GetInmateByID/{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Inmate>> GetInmateByID(int id)
        {
            Inmate inmate = await this._inmatesService.GetInmateByID(id);

            if (inmate == null)
            {
                return NotFound("No Inmate found with this ID.");
            }

            return inmate;
        }

        /// <summary>
        /// Get the Inmate for by FirstName, LastName and BirthDate.
        /// </summary>
        /// <remarks>
        /// Sample Request: api/Inmates/Jhon/Doe/1989-01-01 
        /// 
        /// Notes:
        /// 
        /// 1. First Name and Last Name values are case insensitive.
        /// 
        /// 2.Birth Must be in [yyyy-MM-dd] or [MM-dd-yyyy], A date picker is recommended.
        /// 
        /// Requires: Non-authenticated users 
        ///</remarks>
        /// <response code="404">Inmate not found.</response>  
        /// <response code="400">Parameter are invalid.</response>  
        /// <returns>Inmate Object</returns>
        [HttpGet("GetInmateByNameAndBirthDate/{FirstName}/{LastName}/{DateOfBirth}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Inmate>> GetInmateByNameAndBirthDate(string firstName, string lastName, DateTime dateOfBirth)
        {
            if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || dateOfBirth.Equals(default))
            {
                string error = string.Empty;
                if(string.IsNullOrEmpty(firstName))
                {
                    error = "First Name is null or Empty";
                }
                else if(string.IsNullOrEmpty(lastName))
                {
                    error = "Last Name is null or Empty";
                }
                else
                {
                    error = "Date cannot be default";

                }

                return BadRequest(error);
            }

            Inmate inmates = await _inmatesService.GetInmateByNameAndBirthDate(firstName, lastName, dateOfBirth).ConfigureAwait(false);

            if(inmates == null)
            {
                return NotFound("No Inamte found with this combination of First Name, Last Name and Birth Date.");
            }

            return inmates;
        }

        /// <summary>
        /// Get all the prisoners in the different prision in the city.
        /// </summary>
        /// <remarks>
        /// Sample Request: api/Inmates/GetAllInmates
        /// 
        /// Requires: A user with the role administrator.
        ///</remarks>
        /// <response code="401">Unauthorized Access</response>     
        /// <returns>A list of Inmates</returns>
        [HttpGet("GetAll")]
        [Authorize(Roles = "Administrator")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<Inmate>>> GetAllInmates()
        {
            return await _inmatesService.GetAllInmates().ConfigureAwait(false);
        }


        /// <summary>
        ///Get for the Wardens Location
        /// </summary>
        /// <remarks>
        /// Sample Request: api/Inmates/GetInmatesForMyLocation
        /// 
        /// Requires: JWT Token for Wardens.
        /// </remarks>
        /// <response code="401">Unauthorized Access</response>     
        /// <returns>Returns list of inamtes</returns>
        [HttpGet("GetInmatesForMyLocation")]
        [Authorize(Roles = "warden")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<Inmate>>> GetInmatesForMyLocation()
        {
            string LocationOfUser = string.Empty;

            ClaimsPrincipal currentUser = HttpContext.User;

            if(currentUser.HasClaim(claim => claim.Type.Equals("location")))
            {
                LocationOfUser = currentUser.Claims.FirstOrDefault(claim => claim.Type.Equals("location")).Value;
            }

            if(string.IsNullOrEmpty(LocationOfUser))
            {
                return NotFound("Location not found for the user");
            }

            return await this._inmatesService.GetInmatesByLocation(Location: LocationOfUser);
        }

        [HttpPost("AddInmate")]
        [AllowAnonymous]
        public async Task<ActionResult> AddInmate(Inmate inmate)
        {
            await this._inmatesService.AddInmate(inmate);
            return Ok();
        }
    }
}
