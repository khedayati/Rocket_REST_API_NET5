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
        [HttpPost("{id}/modifyElevatorStatus/{status}")]
        public async Task<dynamic> ChangeElevatorStatus(long id, string status)
        //public async Task<IActionResult> ChangeElevatorStatus(long id, Elevator elevatorItem)
        {
/*
            var elevator = GetElevator(id);
            if (elevator == null)
            {
                return NotFound();
            }



            if (id != elevatorItem.id)
            {
                return BadRequest();
            }

                        var elevatorStatus = await _context.elevators.Where(b => b.id == id).Select(b => b.status).FirstAsync();
            if (elevatorStatus == null)
            {
                Console.WriteLine("elevator is null");
                return NotFound();
            }

            //var elevator = await _context.elevators.FindAsync(id);
            var elevator = await _context.elevators.FindAsync(id);
            if (elevator == null)
            {
                Console.WriteLine("elevator is null");
                return NotFound();
            }
            Console.WriteLine(elevator.status);

            //await Task.Run(() => _context.Entry(status).State = EntityState.Modified); OK
            //_context.elevators.FirstAsync(b => b.id == id)
            //_context.Entry(status).State = EntityState.Modified;
            //_context.Entry(elevatorStatus).State = EntityState.Modified;
            //_context.Entry(elevator.status).State = EntityState.Modified;
            _context.Attach(status);
            _context.Entry(elevator.status).State = EntityState.Modified;



                        var elevator = await _context.elevators.FindAsync(id);
            if (elevator != null) {
                elevator.status = status;
                _context.Entry(elevator).Property("status").IsModified = true;
            }
*/

            var elevator = await _context.elevators.FindAsync(id);
            elevator.status = status;
            
            //await _context.SaveChangesAsync();
            

            //_context.Entry(Elevator)
            
            
            //_context.Entry(await _context.elevators.FirstOrDefaultAsync(x => x.id == id)).CurrentValues.SetValues(elevator);
            //return (await _context.SaveChangesAsync()) > 0;

            //_context.Entry(elevator). = EntityState.Modified;
            //var elevator = new Elevator { id = id };

/*
            _context.Update(elevator); // Use Update here instead of Attach

             if (await TryUpdateModelAsync<Elevator>(
                elevator,
                "elevator",
                s => s.status, s => s.Email))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            if (elevator == null)
            {
                return NotFound();
            }

            var courseToUpdate = await _context.elevators
                .FirstOrDefaultAsync(c => c.id == id);
                try
                {
                    await _context.SaveChangesAsync();
                }
                    if (await TryUpdateModelAsync<Elevator>(courseToUpdate, "", c => c.status))
                {
                try
            {
                await _context.SaveChangesAsync();
            }
                catch (DbUpdateException)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
            return RedirectToAction(nameof(Index));}
*/        

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
            return elevator;
            //return NoContent();
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
