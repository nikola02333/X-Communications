using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XCommunications.Context;
using XCommunications.Interfaces;
using XCommunications.ModelsController;
using XCommunications.ModelsService;
using XCommunications.Patterns.UnitOfWork;
using XCommunications.Services;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimcardsController : ControllerBase
    {
        private IMapper mapper;
        private ISimcardsService service;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SimcardsController(ISimcardsService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Simcards
        [HttpGet]
        public IEnumerable<SimcardControllerModel> GetSimcard()
        {
            log.Info("Reached GetSimcard() in SimcardsController.cs");

            return service.GetAll().Select(x => mapper.Map<SimcardControllerModel>(x));
        }

        // GET: api/Simcards/5
        [HttpGet("{id}")]
        public IActionResult GetSimcard(int id)
        {
            log.Info("Reached GetSimcard(int id) in SimcardsController.cs");

            SimcardControllerModel sim = mapper.Map<SimcardControllerModel>(service.Get(id));

            if (sim == null)
            {
                log.Error("Got null object in GetSimcard(int id) in SimcardsController.cs");
                return NotFound();
            }

            log.Info("Returned Simcard object from GetSimcard(int id) in SimcardsController.cs");

            return Ok(sim);
        }

        // PUT: api/Simcards/5
        [HttpPut("{id}")]
        public IActionResult PutSimcard(int id, SimcardControllerModel sim)
        {
            log.Info("Reached PutSimcard(int id, SimcardControllerModel sim) in SimcardsController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PutSimcard(int id, SimcardControllerModel sim) in SimcardsController.cs");
                return BadRequest(ModelState);
            }

            if (id != sim.Imsi)
            {
                log.Error("Simcard object isn't matched with given id! Error occured in PutSimcard(int id, SimcardControllerModel sim) in SimcardsController.cs");
                return BadRequest();
            }

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<SimcardServiceModel>(sim)));

            if (exists)
            {
                log.Info("Modified Simcard object in PutSimcard(int id, SimcardControllerModel sim) in SimcardsController.cs");
                return NoContent();
            }

            log.Error("Simcard object with given id doesn't exist! Error occured in PutSimcard(int id, SimcardControllerModel sim) in SimcardsController.cs");

            return NotFound();
        }

        // POST: api/Simcards
        [HttpPost]
        public IActionResult PostSimcard([FromBody] SimcardControllerModel sim)
        {
            log.Info("Reached PostSimcard([FromBody] SimcardControllerModel sim) in SimcardsController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PostSimcard([FromBody] SimcardControllerModel sim) in SimcardsController.cs");
                return BadRequest(ModelState);
            }

            service.Add(mapper.Map<SimcardServiceModel>(sim));
            log.Info("Added new Simcard object in PostSimcard([FromBody] SimcardControllerModel sim) in SimcardsController.cs");

            return NoContent();
        }

        // DELETE: api/Simcards/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSimcard(int id)
        {
            log.Info("Reached DeleteSimcard(int id) in SimcardsController.cs");

            if (!service.Delete(id))
            {
                log.Error("Got null object in DeleteSimcard(int id) in SimcardsController.cs");
                return NotFound();
            }

            log.Info("Deleted Simcard object in DeleteSimcard(int id) in SimcardsController.cs");

            return NoContent();
        }
    }
}