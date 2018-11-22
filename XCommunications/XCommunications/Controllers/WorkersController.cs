using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using log4net;
using XCommunications.Business.Interfaces;
using XCommunications.Business.Models;
using XCommunications.WebAPI.Models;
using System;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private IMapper mapper;
        private IService<WorkerServiceModel> service;
        private ILog log;

        public WorkersController(IService<WorkerServiceModel> service, IMapper mapper, ILog log)
        {
            this.service = service;
            this.mapper = mapper;
            this.log = log;
        }

        // GET: api/Workers
        [HttpGet]
        public IEnumerable<WorkerControllerModel> GetWorker()
        {
            try
            {
                log.Info("Reached GetWorker() in WorkersController.cs");
                return service.GetAll().Select(x => mapper.Map<WorkerControllerModel>(x));
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetWorker() in WorkersController.cs",e));
                return null;
            }
        }

        // GET: api/Workers/5
        [HttpGet("{id}")]
        public IActionResult GetWorker(int id)
        {
            try
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
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetWorker(int id) in WorkersController.cs",e));
                return NotFound();
            }
        }

        // PUT: api/Workers/5
        [HttpPut("{id}")]
        public IActionResult PutWorker(int id, WorkerControllerModel worker)
        {
            try
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

                bool exists = service.Update(mapper.Map<WorkerServiceModel>(worker));

                if (exists)
                {
                    log.Info("Modified Worker object in PutWorker(int id, WorkerControllerModel worker) in WorkersController.cs");
                    return NoContent();
                }

                log.Error("Worker object with given id doesn't exist! Error occured in PutWorker(int id, WorkerControllerModel worker) in WorkersController.cs");

                return NotFound();
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PutWorker(int id, WorkerControllerModel worker) in WorkersController.cs",e));
                return NotFound();
            }
        }

        // POST: api/Workers
        [HttpPost]
        public IActionResult PostWorker([FromBody] WorkerControllerModel worker)       
        {
            try
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
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PostWorker([FromBody] WorkerControllerModel worker) in WorkersController.cs",e));
                return NotFound();
            }
        }

        // DELETE: api/Workers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteWorker(int id)
        {
            try
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
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GDeleteWorker(int id) in WorkersController.cs",e));
                return null;
            }
        }
    }
}