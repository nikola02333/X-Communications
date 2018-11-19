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

        public SimcardsController(ISimcardsService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Simcards
        [HttpGet]
        public IEnumerable<SimcardControllerModel> GetSimcards()
        {
            return service.GetAll().Select(x => mapper.Map<SimcardControllerModel>(x));
        }

        // GET: api/Simcards/5
        [HttpGet("{id}")]
        public IActionResult GetSimcard(int id)
        {
            SimcardControllerModel sim = mapper.Map<SimcardControllerModel>(service.Get(id));

            if (sim == null)
            {
                return NotFound();
            }

            return Ok(sim);
        }

        // PUT: api/Simcards/5
        [HttpPut("{id}")]
        public IActionResult PutSimcard(int id, SimcardControllerModel sim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sim.Imsi)
            {
                return BadRequest();
            }

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<SimcardServiceModel>(sim)));

            if (exists)
            {
                return NoContent();
            }

            return NotFound();
        }

        // POST: api/Simcards
        [HttpPost]
        public IActionResult PostSimcard([FromBody] SimcardControllerModel sim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.Add(mapper.Map<SimcardServiceModel>(sim));

            return CreatedAtRoute(new { id = sim.Imsi }, sim);
        }

        // DELETE: api/Simcards/5
        [HttpDelete("{id}")]
        public IActionResult SimcardWorker(int id)
        {
            if (!service.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}