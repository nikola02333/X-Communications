using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XCommunications.Models;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        private readonly XCommunicationsContext _context;

        public NumbersController(XCommunicationsContext context)
        {
            _context = context;
        }

        // GET: api/Numbers
        [HttpGet]
        public IEnumerable<Number> GetNumber()
        {
            return _context.Number;
        }

        // GET: api/Numbers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNumber([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var number = await _context.Number.FindAsync(id);

            if (number == null)
            {
                return NotFound();
            }

            return Ok(number);
        }

        // PUT: api/Numbers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNumber([FromRoute] int id, [FromBody] Number number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != number.Id)
            {
                return BadRequest();
            }

            _context.Entry(number).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NumberExists(id))
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

        // POST: api/Numbers
        [HttpPost]
        public async Task<IActionResult> PostNumber([FromBody] Number number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Number.Add(number);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NumberExists(number.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNumber", new { id = number.Id }, number);
        }

        // DELETE: api/Numbers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNumber([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var number = await _context.Number.FindAsync(id);
            if (number == null)
            {
                return NotFound();
            }

            _context.Number.Remove(number);
            await _context.SaveChangesAsync();

            return Ok(number);
        }

        private bool NumberExists(int id)
        {
            return _context.Number.Any(e => e.Id == id);
        }
    }
}