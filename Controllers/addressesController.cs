using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketApi.Models;

namespace RocketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class addressesController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public addressesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Addresses>>> Getaddresses()
        {
            return await _context.addresses.ToListAsync();
        }

        // GET: api/addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Addresses>> Getaddresses(long id)
        {
            var addresses = await _context.addresses.FindAsync(id);

            if (addresses == null)
            {
                return NotFound();
            }

            return addresses;
        }

        // PUT: api/addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putaddresses(long id, Addresses addresses)
        {
            if (id != addresses.id)
            {
                return BadRequest();
            }

            _context.Entry(addresses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!addressesExists(id))
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

        // POST: api/addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Addresses>> Postaddresses(Addresses addresses)
        {
            _context.addresses.Add(addresses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaddresses", new { id = addresses.id }, addresses);
        }

        // DELETE: api/addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteaddresses(long id)
        {
            var addresses = await _context.addresses.FindAsync(id);
            if (addresses == null)
            {
                return NotFound();
            }

            _context.addresses.Remove(addresses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool addressesExists(long id)
        {
            return _context.addresses.Any(e => e.id == id);
        }
    }
}
