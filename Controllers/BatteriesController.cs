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
/*
    // GET: api/batteries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
    {
        return await _context.batteries.ToListAsync();
    }

    [HttpGet]
    public async Task<dynamic> GetAllBatteries()
    {

      var batteries = await _context.batteries.ToListAsync();

      var i = 0;

      var numbers = new List<Int64>() { };
      foreach (Battery battery in batteries)
      {
        i++;
      }
      numbers.Add(i);

      return numbers;
    }
*/


    //Get: api/Batteries/id       
    //Info for battery *id= battery you want info on, for example: 1*
    [HttpGet("{id}")]
    public ActionResult<List<Battery>> GetBattery(long id)
    {
      List<Battery> batteriesList = new List<Battery>();
      var findBattery = from battery in _context.batteries
                        where id == battery.building_id
                        select battery;
      batteriesList.AddRange(findBattery);
      return batteriesList;
    }

    [HttpGet("{id}/status")]
    public async Task<ActionResult<string>> GetBatteryStatus(long id)
    {
      var battery = await _context.batteries.FindAsync(id);

      if (battery == null)
      {
        return NotFound();
      }

      return battery.status;
    }

    [HttpGet("update/{id}/{status}")]
    public async Task<dynamic> test(string status, long id)
    {
      var battery = await _context.batteries.FindAsync(id);

      if (battery == null) {
        return NotFound();
      }

      if (!(status.Equals("Online") || status.Equals("Offline") || status.Equals("Intervention")) ||
          !(status.Equals("online") || status.Equals("offline") || status.Equals("intervention"))) {
        return Unauthorized();
      }

      battery.status = status;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        throw;
      }
      //await _context.SaveChangesAsync();

      return battery;
    }
  }
}