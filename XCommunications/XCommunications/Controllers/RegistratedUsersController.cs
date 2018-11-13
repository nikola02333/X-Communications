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
    public class RegistratedUsersController : ControllerBase
    {
        private readonly XCommunicationsContext _context;

        public RegistratedUsersController(XCommunicationsContext context)
        {
            _context = context;
        }

        // GET: api/RegistratedUsers
        [HttpGet]
        public IEnumerable<RegistratedUser> GetRegistratedUser()
        {
            return _context.RegistratedUser;
        }

        // GET: api/RegistratedUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistratedUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registratedUser = await _context.RegistratedUser.FindAsync(id);

            if (registratedUser == null)
            {
                return NotFound();
            }

            return Ok(registratedUser);
        }

        // PUT: api/RegistratedUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistratedUser([FromRoute] int id, [FromBody] RegistratedUser registratedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registratedUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(registratedUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistratedUserExists(id))
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

        // POST: api/RegistratedUsers
        [HttpPost]
        public async Task<IActionResult> PostRegistratedUser([FromBody] RegistratedUser registratedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RegistratedUser.Add(registratedUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RegistratedUserExists(registratedUser.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRegistratedUser", new { id = registratedUser.Id }, registratedUser);
        }

        // DELETE: api/RegistratedUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistratedUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registratedUser = await _context.RegistratedUser.FindAsync(id);
            if (registratedUser == null)
            {
                return NotFound();
            }

            _context.RegistratedUser.Remove(registratedUser);
            await _context.SaveChangesAsync();

            return Ok(registratedUser);
        }

        private bool RegistratedUserExists(int id)
        {
            return _context.RegistratedUser.Any(e => e.Id == id);
        }
    }
}