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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public WorkersController(IWorkersService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Workers
        [HttpGet]
        public IEnumerable<WorkerControllerModel> GetWorker()
        {
            log.Info("Reached GetWorker() in WorkersController.cs");

            return service.GetAll().Select(x => mapper.Map<WorkerControllerModel>(x));
        }

        // GET: api/Workers/5
        [HttpGet("{id}")]
        public IActionResult GetWorker(int id)
        {
            log.Info("Reached GetWorker(int id) in WorkersController.cs");

            WorkerControllerModel worker = mapper.Map<WorkerControllerModel>(service.Get(id));

            if (worker == null)
            {
                log.Error("Got null object in GetWorker(int id) in WorkersController.cs");
                return NotFound();
            }

            log.Info("Returned Worker object from GetWorker(int id) in WorkersController.cs");

            return Ok(worker);
        }

        // PUT: api/Workers/5
        [HttpPut("{id}")]
        public IActionResult PutWorker(int id, WorkerControllerModel worker)
        {
            log.Info("Reached PutWorker(int id, WorkerControllerModel worker) in WorkersController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PutWorker(int id, WorkerControllerModel worker) in WorkersController.cs");
                return BadRequest(ModelState);
            }

            if (id != worker.Id)
            {
                log.Error("Worker object isn't matched with given id! Error occured in PutWorker(int id, WorkerControllerModel worker) in WorkersController.cs");
                return BadRequest();
            }

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<WorkerServiceModel>(worker)));

            if (exists)
            {
                log.Info("Modified Worker object in PutWorker(int id, WorkerControllerModel worker) in WorkersController.cs");
                return NoContent();
            }

            log.Error("Worker object with given id doesn't exist! Error occured in PutWorker(int id, WorkerControllerModel worker) in WorkersController.cs");

            return NotFound();
        }

        // POST: api/Workers
        [HttpPost]
        public IActionResult PostWorker([FromBody] WorkerControllerModel worker)       
        {
            log.Info("Reached PostWorker([FromBody] WorkerControllerModel worker) in WorkersController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PostWorker([FromBody] WorkerControllerModel worker) in WorkersController.cs");
                return BadRequest(ModelState);
            }

            service.Add(mapper.Map<WorkerServiceModel>(worker));
            log.Info("Added new Worker object in PostWorker([FromBody] WorkerControllerModel worker) in WorkersController.cs");

            return NoContent();
        }

        // DELETE: api/Workers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteWorker(int id)
        {
            log.Info("Reached DeleteWorker(int id) in WorkersController.cs");

            if (!service.Delete(id))
            {
                log.Error("Got null object in DeleteWorker(int id) in WorkersController.cs");
                return NotFound();
            }

            log.Info("Deleted Worker object in DeleteWorker(int id) in WorkersController.cs");

            return NoContent();
        }
    }
}