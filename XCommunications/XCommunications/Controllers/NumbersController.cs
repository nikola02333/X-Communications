using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XCommunications.Models;
using XCommunications.Patterns.UnitOfWork;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public NumbersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Numbers
        [HttpGet]
        public IEnumerable<Number> GetNumbers()
        {
            return unitOfWork.NumberRepository.GetAll();
        }

        // GET: api/Numbers/5
        [HttpGet("{id}")]
        public IActionResult GetNumber(int id)
        {
            Number number = context.Number.Find(id);

            if (number == null)
            {
                return NotFound();
            }

            return Ok(number);
        }

        // PUT: api/Numbers/5
        [HttpPut("{id}")]
        public IActionResult PutNumber(int id, Number number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != number.Id)
            {
                return BadRequest();
            }

            context.Entry(number).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
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
        public IActionResult PostNumber(Number number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.NumberRepository.Add(number);
            unitOfWork.Commit();

            return CreatedAtRoute(new { id = number.Id }, number);
        }

        // DELETE: api/Numbers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNumber(int id)
        {
            Number number = context.Number.Find(id);

            if (number == null)
            {
                return NotFound();
            }

            context.Number.Remove(number);
            context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.NumberId == number.Id));
            context.SaveChanges();

            return Ok(number);
        }

        private bool NumberExists(int id)
        {
            return context.Number.Count(w => w.Id == id) > 0;
        }
    }
}