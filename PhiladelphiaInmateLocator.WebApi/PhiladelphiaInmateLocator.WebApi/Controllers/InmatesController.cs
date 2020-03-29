namespace PhiladelphiaInmateLocator.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using PhiladelphiaInmateLocator.WebApi.Models;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InmatesController : ControllerBase
    {
        private readonly InmateContext _context;

        public InmatesController(InmateContext context)
        {
            _context = context;
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
        public async Task<ActionResult<List<Inmate>>> SetData ()
        {
            _context.Inmates.Add(new Inmate { Id = 420, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1989, 01, 01), LocationID = 1 });
            _context.Inmates.Add(new Inmate { Id = 421, FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1971, 05, 12), LocationID = 1 });
            _context.Inmates.Add(new Inmate { Id = 422, FirstName = "Julia", LastName = "Roberts", DateOfBirth = new DateTime(1972, 06, 10), LocationID = 1 });
            _context.Inmates.Add(new Inmate { Id = 423, FirstName = "Bill", LastName = "Cosby", DateOfBirth = new DateTime(1965, 10, 10), LocationID = 2 });
            _context.Inmates.Add(new Inmate { Id = 424, FirstName = "Matthew", LastName = "Wade", DateOfBirth = new DateTime(1993, 12, 20), LocationID = 3 });
            _context.Inmates.Add(new Inmate { Id = 425, FirstName = "Michael", LastName = "Clarke", DateOfBirth = new DateTime(1988, 11, 20), LocationID = 3 });
            _context.Inmates.Add(new Inmate { Id = 426, FirstName = "Jack", LastName = "Reacher", DateOfBirth = new DateTime(1991, 12, 30), LocationID = 3 });
            _context.Location.Add(new Location { Id = 1, Name = "City Hall" });
            _context.Location.Add(new Location { Id = 2, Name = "South Prison" });
            _context.Location.Add(new Location { Id = 3, Name = "Alcatraz" });
            await _context.SaveChangesAsync();

            return await _context.Inmates.ToListAsync();
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
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Inmate>> GetInmate(int id)
        {
            var inmate = await _context.Inmates.FindAsync(id);

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
        /// 1. First Name and Last Name are case sensitive.
        /// 
        /// 2.Birth Must be in [yyyy-MM-dd] or [MM-dd-yyyy], A date picker is recommended.
        /// 
        /// Requires: Non-authenticated users 
        ///</remarks>
        /// <response code="404">Inmate not found</response>  
        /// <returns>Inmate Object</returns>
        [HttpGet("GetInmateByNameAndBirthDate/{FirstName}/{LastName}/{DateOfBirth}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Inmate>>> GetInmateByNameAndBirthDate (string firstName, string lastName, DateTime dateOfBirth)
        {
            List<Inmate> inmates = await _context.Inmates
                .Where(inmate => inmate.FirstName.Equals(firstName) && inmate.LastName.Equals(lastName) && inmate.DateOfBirth.Equals(dateOfBirth)).ToListAsync();

            if(inmates == null && !inmates.Any())
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
        [HttpGet("GetAllInmates")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Inmate>>> GetAllInmates ()
        {
            return await _context.Inmates.ToListAsync();
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<List<Inmate>> GetInmatesForMyLocation ()
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

            List<Inmate> inmates = _context.Location
                .Where(loc => loc.Name.Equals(LocationOfUser))
                .Join(_context.Inmates,
                loc => new { Id = loc.Id },
                inmate => new { Id = inmate.LocationID },
                (loc, inmate) => inmate).ToList();

            return inmates;
        }
    }
}
