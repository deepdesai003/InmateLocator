using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhiladelphiaInmateLocator.WebApi.Models;

namespace PhiladelphiaInmateLocator.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InmatesController : ControllerBase
    {
        private readonly InmateContext _context;

        public InmatesController(InmateContext context)
        {
            _context = context;
        }

        // GET: api/Inmates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inmate>>> GetInmates()
        {
            return await _context.Inmates.ToListAsync();
        }

        // GET: api/Inmates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inmate>> GetInmate(int id)
        {
            var inmate = await _context.Inmates.FindAsync(id);

            if (inmate == null)
            {
                return NotFound();
            }

            return inmate;
        }

        [HttpGet("GetInmateByNameAndBirthDate")]
        public async Task<ActionResult<List<Inmate>>> GetInmateByNameAndBirthDate (string firstName, string lastName, DateTime dateOfBirth)
        {
            List<Inmate> inmate = await _context.Inmates
                .Where(inmate => inmate.FirstName.Equals(firstName) && inmate.LastName.Equals(lastName) && inmate.DateOfBirth.Equals(dateOfBirth)).ToListAsync();

            if(inmate == null)
            {
                return NotFound();
            }

            return inmate;
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
