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