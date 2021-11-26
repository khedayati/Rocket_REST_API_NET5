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

    // GET: api/Buildings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Column>>> GetColumn()
    {
      return await _context.columns.ToListAsync();
    }

    [HttpGet("{id}/status")]
    public async Task<ActionResult<string>> GetColumnStatus(long id)
    {
      var column = await _context.columns.FindAsync(id);
      if (column == null)
      {
        return NotFound();
      }
      return column.status;
    }

    [HttpGet("update/{id}/{status}")]
    public async Task<dynamic> test(string status, long id)
    {
      var column = await _context.columns.FindAsync(id);

      column.status = status;
      await _context.SaveChangesAsync();

      return column;
    }
  }
}