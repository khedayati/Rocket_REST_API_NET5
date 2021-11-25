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

        [HttpGet("InterventionBuildings")]
        public async Task<ActionResult<IEnumerable<Buildings>>> GetInterventionBuildings()
        {

            var findBuildings = from building in _context.Buildings
                                from battery in building.Batteries
                                from column in battery.Columns
                                from elevator in column.Elevators
                                where battery.Status.Equals("Intervention") || column.Status.Equals("Intervention") || elevator.Status.Equals("Intervention")
                                select building;

            var distinctBuildings = (from building in findBuildings
                               select building).Distinct();


        return await distinctBuildings.ToListAsync();
        }

        // GET Request: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buildings>>> getBuildings()
        {
            return await _context.Buildings.ToListAsync();
        }

        // [HttpGet("{email}/customer")]
        // public object getBuildingsByEmail(string email)
        // {
        //     return _context.Buildings.Where(building => building.EmailOfTheAdministratorOfTheBuilding == email);
        // }

        // [HttpGet("{email}/customer")]
        // public async Task<ActionResult<IEnumerable<Buildings>>> getBuildingsByEmail(string email)
        // {
        //     return await _context.Buildings.Where(building => building.EmailOfTheAdministratorOfTheBuilding == email).ToListAsync();
        // }
        [HttpGet("{email}")]
        public async Task<ActionResult<List<Buildings>>> getBuildingsByEmail(string email)
        {
        // var findBuildings = from building in _context.Buildings
        //                     join customer in _context.Customers on building.CustomerId equals customer.Id
        //                     where email == customer.EmailOfTheCompany
        //                     select building;

          var findBuildings = await _context.Buildings.FromSqlInterpolated(
                $@"SELECT *
                FROM buildings WHERE customer_id =
                (SELECT Id 
                FROM customers WHERE EmailOfTheCompany = {email})")
                .AsNoTracking()
                .ToListAsync(); 

        //var findBuildings = await _context.Buildings.FromSqlRaw("SELECT * FROM buildings WHERE customer_id = (SELECT Id FROM customers WHERE EmailOfTheCompany = {email})", email).AsNoTracking().ToListAsync();

        return findBuildings;
        }

        [HttpGet("byid/{id}")]
        public async Task<ActionResult<Buildings>> GetBuildings(long id)
        {
            var building = await _context.Buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }
    }

}