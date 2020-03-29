namespace PhiladelphiaInmateLocator.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using PhiladelphiaInmateLocator.WebApi.Models;
    using AuthorizeAttribute = PhiladelphiaInmateLocator.WebApi.Framework.AuthorizeAttribute;

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

        // GET: api/Inmates/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Inmate>> GetInmate(int id)
        {
            var inmate = await _context.Inmates.FindAsync(id);

            if (inmate == null)
            {
                return NotFound();
            }

            return inmate;
        }

        // GET: api/Inmates/
        [HttpGet("GetInmateByNameAndBirthDate/{firstName}/{lastName}/{dateOfBirth}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Inmate>>> GetInmateByNameAndBirthDate (string firstName, string lastName, DateTime dateOfBirth)
        {
            List<Inmate> inmates = await _context.Inmates
                .Where(inmate => inmate.FirstName.Equals(firstName) && inmate.LastName.Equals(lastName) && inmate.DateOfBirth.Equals(dateOfBirth)).ToListAsync();

            if(inmates == null)
            {
                return NotFound();
            }

            return inmates;
        }

        // GET: api/Inmates
        
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<Inmate>>> GetInmates ()
        {
            return await _context.Inmates.ToListAsync();
        }

        // GET: api/Inmates/GetInmatesForMyLocation
        [HttpGet("GetInmatesForMyLocation")]
        [Authorize(Roles = "warden")]
        public ActionResult<List<Inmate>> GetInmatesForMyLocation ()
        {
            string LocationOfUser = User.Claims.Where(claim => claim.Type.Equals("location")).First().Value;


            if(string.IsNullOrEmpty(LocationOfUser))
            {
                return NotFound();
            }

            List<Inmate> inmates = _context.Location
                .Where(loc => loc.Name.Equals(LocationOfUser))
                .Join(_context.Inmates,
                loc => new { Id = loc.Id },
                inmate => new { Id = inmate.LocationID },
                (loc, inmate) => inmate).ToList();

            return inmates;
        }



        // PUT: api/Inmates/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInmate(int id, Inmate inmate)
        {
            if (id != inmate.Id)
            {
                return BadRequest();
            }

            _context.Entry(inmate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InmateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Inmates
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Inmate>> PostInmate(Inmate inmate)
        {
            _context.Inmates.Add(inmate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInmate), new { id = inmate.Id }, inmate);
        }

        [HttpPost("SetData")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Inmate>>> SetData ()
        {
            _context.Inmates.Add(new Inmate { Id = 420, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1989, 01, 01), LocationID = 1 });
            _context.Inmates.Add(new Inmate { Id = 421, FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1971, 05, 12), LocationID = 1 });
            _context.Inmates.Add(new Inmate { Id = 422, FirstName = "Julia", LastName = "Roberts", DateOfBirth = new DateTime(1972, 06, 10), LocationID = 1 });
            _context.Inmates.Add(new Inmate { Id = 423, FirstName = "Bill", LastName = "Cosby", DateOfBirth = new DateTime(1965, 10, 10), LocationID = 2 });
            _context.Inmates.Add(new Inmate { Id = 424, FirstName = "Matthew", LastName = "Wade", DateOfBirth = new DateTime(1993,12, 20), LocationID = 3 });
            _context.Inmates.Add(new Inmate { Id = 425, FirstName = "Michael", LastName = "Clarke", DateOfBirth = new DateTime(1988, 11, 20), LocationID = 3 });
            _context.Inmates.Add(new Inmate { Id = 426, FirstName = "Jack", LastName = "Reacher", DateOfBirth = new DateTime(1991, 12, 30), LocationID = 3 });
            _context.Location.Add(new Location { Id = 1, Name = "City Hall" });
            _context.Location.Add(new Location { Id = 2, Name = "South Prison" });
            _context.Location.Add(new Location { Id = 3, Name = "Alcatraz" });
            await _context.SaveChangesAsync();

            return await _context.Inmates.ToListAsync();
        }

        // DELETE: api/Inmates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Inmate>> DeleteInmate(int id)
        {
            var inmate = await _context.Inmates.FindAsync(id);
            if (inmate == null)
            {
                return NotFound();
            }

            _context.Inmates.Remove(inmate);
            await _context.SaveChangesAsync();

            return inmate;
        }

        private bool InmateExists(int id)
        {
            return _context.Inmates.Any(e => e.Id == id);
        }
    }
}
