using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XCommunications.ModelsController;
using XCommunications.Patterns.UnitOfWork;
using System.Web.Http;
using XCommunications.Context;
using XCommunications.Services;
using AutoMapper;
using XCommunications.ModelsService;
using XCommunications.Interfaces;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private IMapper mapper;
        private IWorkersService service;

        public WorkersController(IWorkersService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Workers
        [HttpGet]
        public IEnumerable<WorkerControllerModel> GetWorkers()
        {
            return service.GetAll().Select(x => mapper.Map<WorkerControllerModel>(x));
        }

        // GET: api/Workers/5
        [HttpGet("{id}")]
        public IActionResult GetWorker(int id)
        {
            WorkerControllerModel worker = mapper.Map<WorkerControllerModel>(service.Get(id));


            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker);
        }

        // PUT: api/Workers/5
        [HttpPut("{id}")]
        public IActionResult PutWorker(int id, WorkerControllerModel worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != worker.Id)
            {
                return BadRequest();
            }

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<WorkerServiceModel>(worker)));

            if (exists)
            {
                return NoContent();
            }

            return NotFound();
        }

        // POST: api/Workers
        [HttpPost]
        public IActionResult PostWorker([FromBody] WorkerControllerModel worker)       
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.Add(mapper.Map<WorkerServiceModel>(worker));

            return NoContent();
        }

        // DELETE: api/Workers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteWorker(int id)
        {
            if (!service.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}