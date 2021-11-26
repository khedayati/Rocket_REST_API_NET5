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
    public class BuildingsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public BuildingsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuilding()
        {
            return await _context.buildings.ToListAsync();
        }

/*
        // GET: api/buildings/buildings-with-at-least-one-battery-column-elevator-intervention
        [HttpGet("buildings-with-at-least-one-battery-column-elevator-intervention")]
        //public HashSet<Building> GetBuidlingsRequiringIntervention()
        public async Task<ActionResult<HashSet<Building>>> GetBuidlingsRequiringIntervention()
        {
            //await .OpenAsync();

            //List<Building> buildingList = new List<Building>();
            HashSet<Building> buildingList = new HashSet<Building>();
            //var List<add_battery> = new List<add_battery>();


"BuildingsController.cs" 205L, 6822C                                                                                              1,1           Top
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Buildings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
            _context.buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuilding", new { id = building.id }, building);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(long id)
        {
            var building = await _context.buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(building);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingExists(long id)
        {
            return _context.buildings.Any(e => e.id == id);
        }
        */
    }
}
