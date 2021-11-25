using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using RocketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace RocketApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomersController : ControllerBase
  {
    private readonly ApplicationContext _context;

    public CustomersController(ApplicationContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
      return await _context.customers.ToListAsync();
    }


  }
}