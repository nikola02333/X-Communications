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
    public class NumbersController : ControllerBase
    {
        private IMapper mapper;
        private INumbersService service;

        public NumbersController(INumbersService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Numbers
        [HttpGet]
        public IEnumerable<NumberControllerModel> GetNumbers()
        {
            return service.GetAll().Select(x => mapper.Map<NumberControllerModel>(x));
        }

        // GET: api/Numbers/5
        [HttpGet("{id}")]
        public IActionResult GetNumber(int id)
        {
            NumberControllerModel number = mapper.Map<NumberControllerModel>(service.Get(id));

            if (number == null)
            {
                return NotFound();
            }

            return Ok(number);
        }

        // PUT: api/Numbers/5
        [HttpPut("{id}")]
        public IActionResult PutNumber(int id, NumberControllerModel number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != number.Id)
            {
                return BadRequest();
            }

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<NumberServiceModel>(number)));

            if (exists)
            {
                return NoContent();
            }

            return NotFound();
        }

        // POST: api/Numbers
        [HttpPost]
        public IActionResult PostNumber([FromBody] NumberControllerModel number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.Add(mapper.Map<NumberServiceModel>(number));

            return NoContent(); ;
        }

        // DELETE: api/Numbers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNumber(int id)
        {
            if (!service.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}