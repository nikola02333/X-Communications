using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using XCommunications.Business.Interfaces;
using XCommunications.Business.Models;
using XCommunications.WebAPI.Models;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class SimcardsController : ControllerBase
    {
        private IMapper mapper;
        private IService<SimcardServiceModel> service;
        private IQuery<SimcardServiceModel> query;
        private ILog log;

        public SimcardsController(IService<SimcardServiceModel> service, IMapper mapper, ILog log, IQuery<SimcardServiceModel> query)
        {
            this.service = service;
            this.mapper = mapper;
            this.log = log;
            this.query = query;
        }

        // GET: api/Simcards
        [HttpGet]
        public IEnumerable<SimcardControllerModel> GetSimcard()
        {
            try
            {
                log.Info("Reached GetSimcard() in SimcardsController.cs");
                return service.GetAll().Select(x => mapper.Map<SimcardControllerModel>(x));
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetSimcard() in SimcardsController.cs",e));
                return null;
            }
        }

        // GET: api/Simcards/5
        [HttpGet("{id}")]
        public IActionResult GetSimcard(int id)
        {
            try
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
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetSimcard(int id) in SimcardsController.cs",e));
                return NotFound();
            }
        }

        [HttpGet("GetAvailableSimcard")]
        public IEnumerable<SimcardControllerModel> GetAvailableSimcard()
        {
            try
            {
                log.Info("Reached GetAvailableSimcard() in SimcardsController.cs");
                return query.FindAvailable().Select(x => mapper.Map<SimcardControllerModel>(x));
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetAvailableSimcard() in SimcardsController.cs", e));
                return null;
            }
        }

        // PUT: api/Simcards/5
        [HttpPut("{id}")]
        public IActionResult PutSimcard(int id, SimcardControllerModel sim)
        {
            try
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

                bool exists = service.Update(mapper.Map<SimcardServiceModel>(sim));

                if (exists)
                {
                    log.Info("Modified Simcard object in PutSimcard(int id, SimcardControllerModel sim) in SimcardsController.cs");
                    return NoContent();
                }

                log.Error("Simcard object with given id doesn't exist! Error occured in PutSimcard(int id, SimcardControllerModel sim) in SimcardsController.cs");

                return Ok(sim);
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PutSimcard(int id, SimcardControllerModel sim) in SimcardsController.cs",e));
                return NotFound();
            }
        }

        // POST: api/Simcards
        [HttpPost]
        public IActionResult PostSimcard([FromBody] SimcardControllerModel sim)
        {
            try
            {
                log.Info("Reached PostSimcard([FromBody] SimcardControllerModel sim) in SimcardsController.cs");

                if (!ModelState.IsValid)
                {
                    log.Error("A ModelState isn't valid error occured in PostSimcard([FromBody] SimcardControllerModel sim) in SimcardsController.cs");
                    return BadRequest(ModelState);
                }

                service.Add(mapper.Map<SimcardServiceModel>(sim));
                log.Info("Added new Simcard object in PostSimcard([FromBody] SimcardControllerModel sim) in SimcardsController.cs");

                return Ok(sim);
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PostSimcard([FromBody] SimcardControllerModel sim) in SimcardsController.cs",e));
                return NotFound();
            }
        }

        // DELETE: api/Simcards/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSimcard(int id)
        {
            try
            {
                log.Info("Reached DeleteSimcard(int id) in SimcardsController.cs");

                if (!service.Delete(id))
                {
                    log.Error("Got null object in DeleteSimcard(int id) in SimcardsController.cs");
                    return NotFound();
                }

                log.Info("Deleted Simcard object in DeleteSimcard(int id) in SimcardsController.cs");

                return Ok();
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in DeleteSimcard(int id) in SimcardsController.cs",e));
                return NotFound();
            }
        }
    }
}