using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XCommunications.Context;
using XCommunications.Models;
using XCommunications.Patterns.UnitOfWork;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistratedUsersController : ControllerBase
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public RegistratedUsersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/RegistratedUsers
        [HttpGet]
        public IEnumerable<RegistratedUser> GetRegistrated()
        {
            return unitOfWork.RegistratedRepository.GetAll();
        }

        // GET: api/RegistratedUsers/5
        [HttpGet("{id}")]
        public IActionResult GetRegistrated(int id)
        {
            RegistratedUser user = context.RegistratedUser.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/RegistratedUsers/5
        [HttpPut("{id}")]
        public IActionResult PutRegistrated(int id, RegistratedUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            context.Entry(user).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistratedExists(id))
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
        public IActionResult PostRegistrated(RegistratedUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.RegistratedRepository.Add(user);
            unitOfWork.Commit();

            return CreatedAtRoute(new { id = user.Id }, user);
        }

        // DELETE: api/RegistratedUsers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRegistrated(int id)
        {
            RegistratedUser user = context.RegistratedUser.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            context.RegistratedUser.Remove(user);
            context.Simcard.RemoveRange(context.Simcard.Where(s => s.Imsi == user.Imsi));
            context.Customer.RemoveRange(context.Customer.Where(s => s.Id == user.CustomerId));
            context.Worker.RemoveRange(context.Worker.Where(s => s.Id == user.WorkerId));
            context.Number.RemoveRange(context.Number.Where(s => s.Id == user.NumberId));

            context.SaveChanges();

            return Ok(user);
        }

        private bool RegistratedExists(int id)
        {
            return context.RegistratedUser.Count(w => w.Id == id) > 0;
        }
    }
}