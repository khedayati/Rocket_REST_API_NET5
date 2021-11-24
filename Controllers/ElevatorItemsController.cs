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
    public class ElevatorItemsController : ControllerBase
    {
        private readonly ElevatorsContext _context;

        public ElevatorItemsController(ElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/ElevatorItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElevatorItem>>> GetElevatorItems()
        {
            return await _context.ElevatorItems.ToListAsync();
        }

        // GET: api/ElevatorItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElevatorItem>> GetElevatorItem(long id)
        {
            var elevatorItem = await _context.ElevatorItems.FindAsync(id);

            if (elevatorItem == null)
            {
                return NotFound();
            }

            return elevatorItem;
        }

        // PUT: api/ElevatorItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElevatorItem(long id, ElevatorItem elevatorItem)
        {
            if (id != elevatorItem.id)
            {
                return BadRequest();
            }

            _context.Entry(elevatorItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorItemExists(id))
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

        // POST: api/ElevatorItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ElevatorItem>> PostElevatorItem(ElevatorItem elevatorItem)
        {
            _context.ElevatorItems.Add(elevatorItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetElevatorItem", new { id = elevatorItem.id }, elevatorItem);
            return CreatedAtAction(nameof(GetElevatorItem), new { id = elevatorItem.id }, elevatorItem);
        }

        // POST: api/TodoItems
//[HttpPost]
//public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
//{
//    _context.TodoItems.Add(todoItem);
//    await _context.SaveChangesAsync();

    //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
//    return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
//}

        // DELETE: api/ElevatorItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElevatorItem(long id)
        {
            var elevatorItem = await _context.ElevatorItems.FindAsync(id);
            if (elevatorItem == null)
            {
                return NotFound();
            }

            _context.ElevatorItems.Remove(elevatorItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElevatorItemExists(long id)
        {
            return _context.ElevatorItems.Any(e => e.id == id);
        }
    }
}
