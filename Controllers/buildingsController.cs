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
        public async Task<ActionResult<List<buildings>>> GetToFixBuildings()
        {
            // Find in batteries where its building_id is equal to building.id (battery that is part of the building)
            // and in columns, find batteries.id  that is equal to column.battery_id (column that is part of the battery)
            // and in elevtors, find column.id  that is equal to elevators.column_id (elevator that is part of the column)
            // and add the building they're part of in the building list.
            IQueryable<buildings> buildings_list = from AllBuildings in _context.buildings
            join batteries in _context.batteries on AllBuildings.id equals batteries.building_id
            join columns in _context.columns on batteries.id equals columns.battery_id
            join elevators in _context.elevators on columns.id equals elevators.column_id
            where (batteries.status.Equals("Intervention") || batteries.status.Equals("intervention")) || 
            (columns.status.Equals("Intervention") || columns.status.Equals("intervention")) || 
            (elevators.status.Equals("Intervention") || elevators.status.Equals("intervention"))
            select AllBuildings;

            return await buildings_list.Distinct().ToListAsync();
        }
    }
}