using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XCommunications.Models;
using XCommunications.Patterns.UnitOfWork;
using System.Web.Http; 

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public WorkersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork=unitOfWork;
        }

        // GET: api/Workers
        [HttpGet]
        public IEnumerable<Worker> GetWorkers()
        {
            return unitOfWork.WorkerRepository.GetAll();
        }

        // GET: api/Workers/5
        [HttpGet("{id}")]
        public IActionResult GetWorker(int id)
        {
            Worker worker = context.Worker.Find(id);

            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker);
        }

        // PUT: api/Workers/5
        [HttpPut("{id}")]
        public IActionResult PutWorker(int id, Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != worker.Id)
            {
                return BadRequest();
            }

            context.Entry(worker).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
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

        // POST: api/Workers
        [HttpPost]
        public IActionResult PostWorker(Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.WorkerRepository.Add(worker);
            unitOfWork.Commit();

            return CreatedAtRoute(new { id = worker.Id }, worker);
        }

        // DELETE: api/Workers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteWorker(int id)
        {
            Worker worker = context.Worker.Find(id);

            if (worker == null)
            {
                return NotFound();
            }

            context.Worker.Remove(worker);
            context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.Worker == worker.Id));
            context.Contract.RemoveRange(context.Contract.Where(s => s.WorkerId == worker.Id));

            context.SaveChanges();

            return Ok(worker);
        }

        private bool WorkerExists(int id)
        {
            return context.Worker.Count(w => w.Id == id) > 0;
        }
    }
}