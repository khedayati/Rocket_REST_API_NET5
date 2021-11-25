using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using RocketApi.Models;

namespace RocketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ElevatorsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevators()
        {
            return await _context.elevators.ToListAsync();
        }

        // GET: api/elevators/5
        [HttpGet("{id}/status")]
        public async Task<string> GetElevatorStatus(long id)
        {
            //var elevatorItem = await _context.elevators.FindAsync(id).AsTask();
            
            //if (elevatorItem == null)
            //{
            //    return NotFound();
            //}
            //var status = elevatorItem.status.AsQueryable();
            //return await _context.elevators.

            //var result = await _context.elevators.AsQueryable().Where(b => b.id == id).AsAsyncEnumerable().GroupBy(b => b.status).ToListAsync();
            //var result = _context.elevators.AsQueryable().Where(b => b.id == id).ToListAsync();
            //return await result.;
            /*
            var result = await _context.elevators
                               .AsQueryable()
                               .Where(b => b.id == id).GroupBy(b => b.status)
                               .ToListAsync();//.GroupBy(b => b.status)
                               //.ToListAsync();
            if (result == null) {
                return NotFound();
            }
            */
            //return result;
            //var elevator = await _context.elevators.AsQueryable().Where(b => b.id == id).ToListAsync();
            //var elevator = await _context.elevators.AsQueryable().Where(b => b.id == id).GroupBy(b => b.status).ToListAsync();
            //var elevator = await _context.elevators.AsQueryable().Where(b => b.id == id).Select(b => b.status);
            //var elevator = await _context.elevators.Where(b => b.id == id).Select(b => b.status).LastAsync();
            //var elevator2 = await _context.elevators.Where(b => b.id == id).Select(b => b.status);
            //var elevator = await _context.elevators.Where(b => b.id == id).Select(b => b.status).LastAsync();
            //var elevator = await _context.elevators.FirstOrDefaultAsync(b => b.id == id);
            var elevator = await _context.elevators.Where(b => b.id == id).Select(b => b.status).FirstAsync();
            //_context.elevators.
            if (elevator == null) {
                //return NotFound();
            }
            Console.WriteLine("elevator status = ", elevator);
            return elevator;
            //return result.AsQueryable().ToString();
            //IQueryable<T> query = ;
            //return await GetDynamicAsync
            //await _context.elevators.Where(b => b.status).ToListAsync();
            //var elevatorCurrentStatus = await elevatorItem.GetAsync(status);
            //return elevatorItem;
            //return elevatorCurrentStatus.Content;
        }


        // GET: api/elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevator(long id)
        {
            var elevatorItem = await _context.elevators.FindAsync(id);

            if (elevatorItem == null)
            {
                return NotFound();
            }

            return elevatorItem;
        }


        // PUT: api/elevators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElevator(long id, Elevator elevatorItem)
        {
            if (id != elevatorItem.id)
            {
                return BadRequest();
            }

            _context.Entry(elevatorItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorExists(id))
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

        // POST: api/elevators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Elevator>> PostElevator(Elevator elevatorItem)
        {
            _context.elevators.Add(elevatorItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetElevatorItem", new { id = elevatorItem.id }, elevatorItem);
            return CreatedAtAction(nameof(GetElevator), new { id = elevatorItem.id }, elevatorItem);
        }

        // DELETE: api/elevators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElevator(long id)
        {
            var elevatorItem = await _context.elevators.FindAsync(id);
            if (elevatorItem == null)
            {
                return NotFound();
            }

            _context.elevators.Remove(elevatorItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElevatorExists(long id)
        {
            return _context.elevators.Any(e => e.id == id);
        }
    }
}
