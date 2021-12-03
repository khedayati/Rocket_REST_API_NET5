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
    public class InterventionsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public InterventionsController(ApplicationContext context)
        {
            _context = context;
        }

        //-------------------------------------------------- Get all elevators ----------------------------------------------------\\

        // GET: api/elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevators()
        {
            return await _context.elevators.ToListAsync();
        }

        //----------------------------------- Retrieving all information from a specific Elevator -----------------------------------\\

        // GET: api/elevators/5/status
        [HttpGet("{id}/status")]
        public async Task<ActionResult<string>> GetElevatorStatus(long id)
        {
            var elevatorStatus = await _context.elevators.Where(b => b.id == id).Select(b => b.status).FirstAsync();
            
            if (elevatorStatus == null) {
                return NotFound();
            }
            return elevatorStatus;
        }

        //----------------------------------- Retrieving the current status of a specific Elevator -----------------------------------\\

        // GET: api/elevators/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevator(long id)
        {
            // Find elevator by its id
            var elevatorItem = await _context.elevators.FindAsync(id);

            if (elevatorItem == null)
            {
                return NotFound();
            }

            return elevatorItem;
        }

        //------------------- Retrieving a list of Elevators that are not in operation at the time of the request -------------------\\

        // GET: api/elevators/elevators-not-in-use
        [HttpGet("elevators-not-in-use")]
        public async Task<dynamic> GetElevatorsNotInUse()
        {
            return await _context.elevators.Where(b => ((b.status == "Offline") || (b.status == "Intervention"))).ToListAsync();
        }

        //------------------- Retrieving a list of Interventions that have a start date of null and their status is Pending -------------------\\
        
        // GET: api/interventions/get-pending-interventions
        [HttpGet("get-pending-interventions")]
        public async Task<ActionResult<List<Intervention>>> GetPendingInterventions()
        {
            // Find all interventions where their start_date are null and their status are "Pending"
            return await _context.interventions.Where(b => (b.start_date == null && b.status == "Pending")).ToListAsync();
        }

/*
        // GET: api/interventions/get-pending-interventions
        [HttpGet("get-pending-interventions")]
        public async Task<ActionResult<List<Intervention>>> GetPendingInterventions()
        {
            // Find in interventions where its start_date is null and its status is "Pending"
            var inter = await _context.interventions.Where(b => (b.start_date == null && b.status == "Pending")).ToListAsync();
            if (inter == null) {
                return NotFound();
            }
            return inter;
        }
        // PUT: Change the status of the intervention request to "InProgress" and add a start date and time (Timestamp).
        // PUT: api/elevators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeStatusOfIntervention(long id, Intervention intervention)
        {
            if (id != intervention.id)
            {
                return BadRequest();
            }

            _context.Entry(intervention).State = EntityState.Modified;

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
*/
        //----------------------------------- Changing the status of a specific Intervention to InProgress -----------------------------------\\

        // PUT: Change the status of the intervention request to "InProgress" and add a start date and time (Timestamp).
        // Put: api/interventions/update-to-pending/id
        [HttpPut("update-to-pending/{id}")]
        public async Task<ActionResult<Intervention>> ChangeInterventionStatusToPending(long id,  Intervention intervention)
        //public async Task<dynamic> ChangeInterventionStatusToPending(long id, [FromBody] Intervention intervention)
        {
            // Find intervention by its id
            var inter = await _context.interventions.FindAsync(id);

            if (inter == null) {
                return NotFound();
            }

            // Change intervention status to InProgress
            inter.status = intervention.status; //"InProgress";

            // Add a timestamp on the intervention
            DateTime currentDate = DateTime.Now;
            inter.start_date = currentDate;

            // Save intervention 
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return inter;
        }

        //----------------------------------- Changing the status of a specific Intervention to Completed -----------------------------------\\

        // PUT: Change the status of the intervention request to "Completed" and add a start date and time (Timestamp).
        // Put: api/interventions/update-to-completed/id
        [HttpPut("update-to-completed/{id}")]
        public async Task<dynamic> ChangeInterventionStatusToCompleted(long id)
        {
            // Find intervention by its id
            var inter = await _context.interventions.FindAsync(id);

            if (inter == null) {
                return NotFound();
            }

            // Change intervention status to Completed
            inter.status = "Completed";

            // Add a timestamp on the intervention
            DateTime currentDate = DateTime.Now;
            inter.start_date = currentDate;

            // Save intervention 
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                throw;
            }
            return inter;
        }
        
        // PUT: Change the status of the request for action to "Completed" and add an end date and time (Timestamp).

        //----------------------------------- Changing the status of a specific Elevator -----------------------------------\\

        // GET: api/elevators/update/id/status
        [HttpGet("update/{id}/{status}")]
        public async Task<dynamic> ChangeElevatorStatus(long id, string status)
        {
            // Find elevator by its id
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            // Change elevator status
            elevator.status = status;

            // Save elevator status            
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
    }
}