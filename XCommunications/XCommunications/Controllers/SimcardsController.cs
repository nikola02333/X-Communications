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
    public class SimcardsController : ControllerBase
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public SimcardsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Simcards
        [HttpGet]
        public IEnumerable<Simcard> GetSimcards()
        {
            return unitOfWork.SimcardRepository.GetAll();
        }

        // GET: api/Simcards/5
        [HttpGet("{id}")]
        public IActionResult GetSimcard(int id)
        {
            Simcard sim = context.Simcard.Find(id);

            if (sim == null)
            {
                return NotFound();
            }

            return Ok(sim);
        }

        // PUT: api/Simcards/5
        [HttpPut("{id}")]
        public IActionResult PutSimcard(int id, Simcard sim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sim.Imsi)
            {
                return BadRequest();
            }

            context.Entry(sim).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
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
        public IActionResult PostSimcard(Simcard sim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.SimcardRepository.Add(sim);
            unitOfWork.Commit();

            return CreatedAtRoute(new { id = sim.Imsi }, sim);
        }

        // DELETE: api/Simcards/5
        [HttpDelete("{id}")]
        public IActionResult SimcardWorker(int id)
        {
            Simcard sim = context.Simcard.Find(id);

            if (sim == null)
            {
                return NotFound();
            }

            context.Simcard.Remove(sim);
            context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.Imsi == sim.Imsi));

            context.SaveChanges();

            return Ok(sim);
        }

        private bool SimcardExists(int id)
        {
            return context.Simcard.Count(w => w.Imsi == id) > 0;
        }
    }
}