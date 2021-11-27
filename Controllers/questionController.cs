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
    public class questionController : ControllerBase
    {
        private readonly PostGresqlContext _pContext;
        private readonly ApplicationContext _context;

        public questionController(PostGresqlContext pContext, ApplicationContext context)
        {
            // Declare the mysql database context and the psql database context.
            _context = context;
            _pContext = pContext;
        }

        // GET /api/question/1/5
        [HttpGet("1/{id}")]
        // Intervention id
        public async Task<Tuple<String, Addresses, String, fact_interventions>> GetFirstQuestion(long id)
        {
            // Get the first question of the graphql query.

            // get the building address associated with the intervention.
            var building_id = await _pContext.fact_interventions.Where(c => c.intervention_id == id).Select(c => c.building_id).FirstAsync();
            var building = await _context.buildings.Where(c => c.id == building_id).FirstAsync();
            var addresses = await _context.addresses.Where(c => c.id == building.address_id).FirstAsync();
            // Get the intervention selected information
            var intervention = await _pContext.fact_interventions.Where(c => c.intervention_id == id).FirstAsync();
            return new Tuple<String, Addresses, String, fact_interventions>("Building Addresses: ",addresses, "Intervention:", intervention);
        }

        // GET /api/question/2/5
        [HttpGet("2/{id}")]
        // Building id
        public async Task<Tuple<String, Customer, String, IEnumerable<fact_interventions>>> GetSecondQuestion(long id)
        {
            // Create a list that will contain all intervention from a building
            List<fact_interventions> building_intervention_list = new List<fact_interventions>();
            // Get customer associated with building
            long customer_id = await _context.buildings.Where(c => c.id == id).Select(c => c.customer_id).FirstAsync();
            // get customer information
            Customer customer = await _context.customers.Where(c => c.id == customer_id).FirstAsync();

            // Add every intervetion related to the building into the building_intervention_list
            foreach (fact_interventions intervention in await _pContext.fact_interventions.Where(c => c.building_id == id).ToListAsync())
            {
                building_intervention_list.Add(intervention);
            }
            return new Tuple<String, Customer, String, IEnumerable<fact_interventions>>("Customer:", customer, "All Building intervention:", building_intervention_list);
        }
        // GET /api/question/3/5
        [HttpGet("3/{id}")]
        // employee id 
        public async Task<List<Tuple<String, fact_interventions,String,  buildings,String, BuildingDetails>>> GetThirdQuestion(long id)
        {
            // Will hold all the intervention an employee did with the building and buiding details associated to it.
            List<Tuple<String, fact_interventions, String, buildings,String, BuildingDetails>> intervention_list = new List<Tuple<String,fact_interventions, String, buildings, String, BuildingDetails>>();
            // All intervention made by the employee
            IEnumerable<fact_interventions> all_interventions = await _pContext.fact_interventions.Where(c => c.employee_id == id).ToListAsync();
            // loop over all intervention selected to get the information of the building, buildings_details and intervention 
            foreach (fact_interventions intervention in all_interventions)
            {
                var building_id_T =  intervention.building_id;
                var building_details = await _context.building_details.Where(c => c.building_id == building_id_T).FirstAsync();
                Console.WriteLine(building_details);
                // buildings building = await _context.buildings.Where(c => c.id == building_id).FirstAsync();
                IQueryable<buildings> building = from AllBuildings in _context.buildings
                where (AllBuildings.id == building_id_T)
                select AllBuildings;
                // Add the 3 field we got into a tuple and add it the the intervention_list
                var tuple = new Tuple<String, fact_interventions, String, buildings, String,  BuildingDetails>("Intervention Infos:", intervention, "Building: ", await building.FirstAsync(), "Building Details:", building_details);
                intervention_list.Add(tuple);
            }
            return intervention_list;

        }

    }
}