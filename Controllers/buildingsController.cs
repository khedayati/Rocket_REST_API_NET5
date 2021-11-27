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

        //-------------------------------------------------- Get all buildings ----------------------------------------------------\\
        
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

        //--------- Retrieving a list of Buildings that contain at least one battery, column or elevator requiring intervention ---------\\
        
        // GET: api/buildings/get-intervention-buildings
        [HttpGet("get-intervention-buildings")]
        //public async Task<IEnumerable<buildings>> GetBuildings()
        //{
                    // GET: api/Buildings/InterventionList
        //[HttpGet("InterventionList")]
        public async Task<ActionResult<List<buildings>>> GetToFixBuildings()
        {
            IQueryable<buildings> buildings_list = from AllBuildings in _context.buildings
            join batteries in _context.batteries on AllBuildings.id equals batteries.building_id
            join columns in _context.columns on batteries.id equals columns.battery_id
            join elevators in _context.elevators on columns.id equals elevators.column_id
            where (batteries.status.Equals("Intervention") || batteries.status.Equals("intervention")) || 
            (columns.status.Equals("Intervention") || columns.status.Equals("intervention")) || 
            (elevators.status.Equals("Intervention") || elevators.status.Equals("intervention"))
            select AllBuildings;

            return await buildings_list.Distinct().ToListAsync();
            /*
        //}
            List<buildings> buildings_list = new List<buildings>();

            // Begin searching in buildings 
            foreach (buildings building in await _context.buildings.ToListAsync())
            {
                var i = 0;
                var j = 0;

                // Loop through in batteries where its building_id is equal to building.id 
                foreach (Battery battery in await _context.batteries.Where(b => b.building_id == building.id).ToListAsync())
                {
                    // Seek any battery that has an "intervention" status
                    if (battery.status == "Intervention")
                    {
                        buildings_list.Add(building);
                        i = 1;
                        break;
                    }
                }
                // Loop through in batteries where its building_id is equal to building.id 
                foreach (Battery battery in await _context.batteries.Where(b => b.building_id == building.id).ToListAsync())
                {
                    // break from the loop since the column with an "intervention" status was found
                    if (i == 1)
                    {
                        break;
                    }
                    var l = await _context.columns.Where(c => c.battery_id == battery.id).ToListAsync();

                    // A battery contains a column, we must then loop through columns if in the loop above a battery was found
                    foreach (Column column in await _context.columns.Where(c => c.battery_id == battery.id).ToListAsync())
                    {
                        // Seek any column that has an "intervention" status
                        if (column.status == "Intervention")
                        {   
                            i = 1;
                            buildings_list.Add(building);
                            break;
                        }
                    }
                }
                // Loop through in batteries again but give the opportunity to search through elevators.
                // Since a Battery may contain Columns and Elevators, if we found any one of the first two objects, we broke
                // from their loops and we need to loop through them again to see if an elevator has the 'intervention' status
                foreach (Battery battery in await _context.batteries.Where(b => b.building_id == building.id).ToListAsync())
                {
                    // break from the loop since the elevator with an "intervention" status was found
                    if (j == 1)
                    {
                        break;
                    }
                    foreach (Column column in await _context.columns.Where(c => c.battery_id == battery.id).ToListAsync())
                    {
                        // break from the loop since the elevator with an "intervention" status was found
                        if (j == 1)
                        {
                            break;
                        }
                        foreach (Elevator elevator in await _context.elevators.Where(e => e.column_id == column.id).ToListAsync())
                        {
                            // Seek any elevator that has an "intervention" status
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
            */
        }
    }
}
