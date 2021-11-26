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


            //buildingList = await _context.buildings.SelectMany(bat => bat)


            
                        var add_battery;
            foreach (Building building_ in _context.buildings) {
                add_battery = await _context.batteries.AllAsync(b => b.status == "Intervention");
            }
            buildingList.Add(add_battery);

                        buildingList = await _context.buildings
                                 .Select(battery => new {
                                     //var battery_ = _context.batteries;
                                     battery_.batteries.Select(b => b.status == "Intervention").ToList()
            buildingList = _context.buildings
                           .Select(async bat =>
                           {
                               foreach (bat in _context.batteries) {
                                   if (bat.st)
                               }
                           });
                           */
            //await _context.buildings
            //      .Include(b => b.)
            //_context.buildings.Log = s => System.Diagnostics.Debug.WriteLine(s);

/*

                        IQueryable query = from b in _context.buildings
                                where b.customer_id == 1
                                select b;
            System.Diagnostics.Trace.WriteLine(query.ToString());



            buildingList = _context.buildings
                           .Include(building => building.battery) 




*/
            //var sql = ((System.Data.Entity.Infrastructure.DbQuery<Building>)query).Sql;
            //((System.Data.Objects.ObjectQuery)query).ToTraceString();
                                 //});
            //await _context.building
            //      .AsQueryable()
/*
            foreach (Building building_ in _context.buildings) {
                foreach (Battery battery_ in _context.batteries) {
                    if (battery_.status.Equals("Intervention")) {
                        buildingList.Add(building_);
                        //buildingList.;
                        //return true;
                    }
                    foreach (Column column_ in _context.columns) {
                        if (column_.status.Equals("Intervention")) {
                            buildingList.Add(building_);
                            //return true;
                        }
                        foreach (Elevator elevator_ in _context.elevators) {
                            if (elevator_.status.Equals("Intervention")) {
                                buildingList.Add(building_);
                                //return true;
                            }
                        }
                    }
                }
            }

            return buildingList;
            //return false;
            /*
            var building = await _context.building
                                       .FirstOrDefaultAsync()
                                       .FirstAsync();
            if (building == null) {
                return NotFound();
            }
            //Console.WriteLine("elevator status = ", elevatorStatus.ToString());
            //return elevatorStatus;
            return building;
            
        }
*/
        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(long id)
        {
            var building = await _context.buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(long id, Building building)
        {
            if (id != building.id)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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
    }
}
