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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NumbersController(INumbersService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Numbers
        [HttpGet]
        public IEnumerable<NumberControllerModel> GetNumber()
        {
            log.Info("Reached GetNumbers() in NumbersController.cs");

            return service.GetAll().Select(x => mapper.Map<NumberControllerModel>(x));
        }

        // GET: api/Numbers/5
        [HttpGet("{id}")]
        public IActionResult GetNumber(int id)
        {
            log.Info("Reached GetNumber(int id) in NumbersController.cs");

            NumberControllerModel number = mapper.Map<NumberControllerModel>(service.Get(id));

            if (number == null)
            {
                log.Error("Got null object in GetNumber(int id) in NumbersController.cs");
                return NotFound();
            }

            log.Info("Returned Number object from GetNumber(int id) in NumbersController.cs");

            return Ok(number);
        }

        // PUT: api/Numbers/5
        [HttpPut("{id}")]
        public IActionResult PutNumber(int id, NumberControllerModel number)
        {
            log.Info("Reached PutNumber(int id, NumberControllerModel number) in NumbersController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PutNumber(int id, NumberControllerModel number) in NumbersController.cs");
                return BadRequest(ModelState);
            }

            if (id != number.Id)
            {
                log.Error("Number object isn't matched with given id! Error occured in PutNumber(int id, NumberControllerModel number) in NumbersController.cs");
                return BadRequest();
            }

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<NumberServiceModel>(number)));

            if (exists)
            {
                log.Info("Modified Number object in PutNumber(int id, NumberControllerModel number) in NumbersController.cs");
                return NoContent();
            }

            log.Error("Number object with given id doesn't exist! Error occured in PutNumber(int id, NumberControllerModel number) in NumbersController.cs");

            return NotFound();
        }

        // POST: api/Numbers
        [HttpPost]
        public IActionResult PostNumber([FromBody] NumberControllerModel number)
        {
            log.Info("Reached PostNumber([FromBody] NumberControllerModel number) in NumbersController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PostNumber([FromBody] NumberControllerModel number) in NumbersController.cs");
                return BadRequest(ModelState);
            }

            service.Add(mapper.Map<NumberServiceModel>(number));
            log.Info("Added new Number object in PostNumber([FromBody] NumberControllerModel number) in NumbersController.cs");

            return NoContent(); 
        }

        // DELETE: api/Numbers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNumber(int id)
        {
            log.Info("Reached DeleteNumber(int id) in NumbersController.cs");

            if (!service.Delete(id))
            {
                log.Error("Got null object in DeleteNumber(int id) in NumbersController.cs");
                return NotFound();
            }

            log.Info("Deleted Number object in DeleteNumber(int id) in NumbersController.cs");

            return NoContent();
        }
    }
}