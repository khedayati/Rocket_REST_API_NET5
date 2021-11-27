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
  public class ColumnsController : ControllerBase
  {
    private readonly ApplicationContext _context;

    public ColumnsController(ApplicationContext context)
    {
      _context = context;
    }

    //-------------------------------------------------- Get all columns ----------------------------------------------------\\
    
    // GET: api/Columns
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Column>>> GetColumn()
    {
      return await _context.columns.ToListAsync();
    }

    //----------------------------------- Retrieving the current status of a specific Column -----------------------------------\\

    //Get: api/Columns/id/status
    [HttpGet("{id}/status")]
    public async Task<ActionResult<string>> GetColumnStatus(long id)
    {
      // Find column by its id
      var column = await _context.columns.FindAsync(id);
      if (column == null)
      {
        return NotFound();
      }
      return column.status;
    }

    //----------------------------------- Changing the status of a specific Column -----------------------------------\\

    //Get: api/Columns/update/id/status
    [HttpGet("update/{id}/{status}")]
    public async Task<dynamic> test(string status, long id)
    {
      // Find column by its id
      var column = await _context.columns.FindAsync(id);

      // Check if the given status is either online, offline or intervention
      if (!(status.Equals("Online") || status.Equals("online")) && 
          !(status.Equals("Offline") || status.Equals("offline")) &&
		      !(status.Equals("Intervention") || status.Equals("intervention"))) {
            return Unauthorized();
      }
      // Change column status
      column.status = status;
      
      // Save column status
      try
      {
      await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        throw;
      }

      return column;
    }
  }
}