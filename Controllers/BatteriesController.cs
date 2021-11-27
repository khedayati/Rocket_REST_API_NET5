using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using RocketApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RocketApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class BatteriesController : ControllerBase
  {
    private readonly ApplicationContext _context;

    public BatteriesController(ApplicationContext context)
    {
      _context = context;
    }

    //-------------------------------------------------- Get all batteries ----------------------------------------------------\\
    
    // GET: api/Batteries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
    {
      // Get all batteries
      return await _context.batteries.ToListAsync();
    }

    //----------------------------------- Retrieving all information from a specific Battery -----------------------------------\\

    //Get: api/Batteries/id       
    //Info for battery *id= battery you want info on, for example: 1*
    [HttpGet("{id}")]
    public ActionResult<List<Battery>> GetBattery(long id)
    {
      List<Battery> batteriesList = new List<Battery>();

      // Find battery by its id
      var findBattery = from battery in _context.batteries
                        where id == battery.building_id
                        select battery;
      batteriesList.AddRange(findBattery);
      return batteriesList;
    }

    //----------------------------------- Retrieving the current status of a specific Battery -----------------------------------\\

    //Get: api/Batteries/id/status
    [HttpGet("{id}/status")]
    public async Task<ActionResult<string>> GetBatteryStatus(long id)
    {
      // Find battery by its id
      var battery = await _context.batteries.FindAsync(id);

      if (battery == null)
      {
        return NotFound();
      }

      return battery.status;
    }
    
    //----------------------------------- Changing the status of a specific Battery -----------------------------------\\

    //Get: api/Batteries/update/id/status
    [HttpGet("update/{id}/{status}")]
    public async Task<dynamic> test(string status, long id)
    {
      // Find battery by its id
      var battery = await _context.batteries.FindAsync(id);

      if (battery == null) {
        return NotFound();
      }

      // Change battery status
      battery.status = status;

      // Save battery status
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        throw;
      }

      return battery;
    }
  }
}