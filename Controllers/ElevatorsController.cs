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

        // GET: api/elevators/5/status
        [HttpGet("{id}/status")]
        public async Task<ActionResult<string>> GetElevatorStatus(long id)
        {
            var elevatorStatus = await _context.elevators.Where(b => b.id == id).Select(b => b.status).FirstAsync();
            if (elevatorStatus == null) {
                return NotFound();
            }
            Console.WriteLine("elevator status = ", elevatorStatus.ToString());
            return elevatorStatus;
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
        //[HttpPut("{id}")]
        //[HttpGet("{id}/modifyElevatorStatus/{status}")]

        [HttpGet("elevatorsnotinuse")]
        public async Task<dynamic> GetElevatorsNotInUse()
        {
            var statusOffline = "Offline";
            var statusIntervention = "Intervention";
            return await _context.elevators.Where(b => ((b.status == statusOffline) || (b.status == statusIntervention))).ToListAsync();
        }
        
        [HttpPost("{id}/{status}/modifyelevatorstatus")]
        public async Task<dynamic> ChangeElevatorStatus(long id, string status)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }
            elevator.status = status;
            //await _context.SaveChangesAsync();
            //return elevator;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return elevator;
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
    /*
        [HttpPost]
        public async Task<ActionResult<Elevator>> PostElevator(Elevator elevatorItem)
        {
            _context.elevators.Add(elevatorItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetElevatorItem", new { id = elevatorItem.id }, elevatorItem);
            return CreatedAtAction(nameof(GetElevator), new { id = elevatorItem.id }, elevatorItem);
        }
    */
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
