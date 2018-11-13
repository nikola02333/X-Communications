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
    public class SimcardsController : ControllerBase
    {
        private readonly XCommunicationsContext _context;

        public SimcardsController(XCommunicationsContext context)
        {
            _context = context;
        }

        // GET: api/Simcards
        [HttpGet]
        public IEnumerable<Simcard> GetSimcard()
        {
            return _context.Simcard;
        }

        // GET: api/Simcards/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSimcard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var simcard = await _context.Simcard.FindAsync(id);

            if (simcard == null)
            {
                return NotFound();
            }

            return Ok(simcard);
        }

        // PUT: api/Simcards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSimcard([FromRoute] int id, [FromBody] Simcard simcard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != simcard.Imsi)
            {
                return BadRequest();
            }

            _context.Entry(simcard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SimcardExists(id))
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

        // POST: api/Simcards
        [HttpPost]
        public async Task<IActionResult> PostSimcard([FromBody] Simcard simcard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Simcard.Add(simcard);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SimcardExists(simcard.Imsi))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSimcard", new { id = simcard.Imsi }, simcard);
        }

        // DELETE: api/Simcards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSimcard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var simcard = await _context.Simcard.FindAsync(id);
            if (simcard == null)
            {
                return NotFound();
            }

            _context.Simcard.Remove(simcard);
            await _context.SaveChangesAsync();

            return Ok(simcard);
        }

        private bool SimcardExists(int id)
        {
            return _context.Simcard.Any(e => e.Imsi == id);
        }
    }
}