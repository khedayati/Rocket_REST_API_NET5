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
    public class buildingsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public buildingsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<buildings>>> Getbuildings()
        {
            return await _context.buildings.ToListAsync();
        }

        //----------------------------------- Retrieving all information from a specific Building -----------------------------------\\

        // GET: api/buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<buildings>> Getbuildings(long id)
        {
            var buildings = await _context.buildings.FindAsync(id);

            if (buildings == null)
            {
                return NotFound();
            }

            return buildings;
        }


        public buildings buildingsFindById(long id, List<buildings> listBuilding)
        {
            foreach (buildings building in listBuilding)
            {
                if (building.id == id)
                {
                    return building;
                }
            }
            return null;
        }

        //--------- Retrieving a list of Buildings that contain at least one battery, column or elevator requiring intervention ---------\\

        [HttpGet("get-intervention-buildings")]
        public async Task<IEnumerable<buildings>> GetBuildings()
        {
            List<buildings> buildings_list = new List<buildings>();
            foreach (buildings building in await _context.buildings.ToListAsync())
            {
                var i = 0;
                var j = 0;
                foreach (Battery battery in await _context.batteries.Where(b => b.building_id == building.id).ToListAsync())
                {
                    if (battery.status == "Intervention")
                    {
                        buildings_list.Add(building);
                        i = 1;
                        break;
                    }
                }
                foreach (Battery battery in await _context.batteries.Where(b => b.building_id == building.id).ToListAsync())
                {
                    if (i == 1)
                    {
                        break;
                    }
                    var l = await _context.columns.Where(c => c.battery_id == battery.id).ToListAsync();
                    foreach (Column column in await _context.columns.Where(c => c.battery_id == battery.id).ToListAsync())
                    {
                        if (column.status == "Intervention")
                        {   
                            i = 1;
                            buildings_list.Add(building);
                            break;
                        }
                    }
                }
                foreach (Battery battery in await _context.batteries.Where(b => b.building_id == building.id).ToListAsync())
                {
                    if (j == 1)
                    {
                        break;
                    }
                    foreach (Column column in await _context.columns.Where(c => c.battery_id == battery.id).ToListAsync())
                    {
                        if (j == 1)
                        {
                            break;
                        }
                        foreach (Elevator elevator in await _context.elevators.Where(e => e.column_id == column.id).ToListAsync())
                        {
                            if (elevator.status == "Intervention")
                            {
                                buildings_list.Add(building);
                                j = 1;
                                i =1;
                                break;
                            }
                        }
                    }

                }
            }
            return buildings_list;
        }
        
        //-------------------------------------------------------------------------------------------------------------------------------\\

        // PUT: api/buildings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putbuildings(long id, buildings buildings)
        {
            if (id != buildings.id)
            {
                return BadRequest();
            }

            _context.Entry(buildings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!buildingsExists(id))
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

        // POST: api/buildings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<buildings>> Postbuildings(buildings buildings)
        {
            _context.buildings.Add(buildings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getbuildings", new { id = buildings.id }, buildings);
        }

        // DELETE: api/buildings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletebuildings(long id)
        {
            var buildings = await _context.buildings.FindAsync(id);
            if (buildings == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(buildings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool buildingsExists(long id)
        {
            return _context.buildings.Any(e => e.id == id);
        }
    }
}
