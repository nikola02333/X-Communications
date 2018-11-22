using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Mvc;
using XCommunications.Business.Interfaces;
using XCommunications.Business.Models;
using XCommunications.WebAPI.Models;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumbersController : ControllerBase
    {
        private IMapper mapper;
        private IService<NumberServiceModel> service;
        private ILog log;

        public NumbersController(IService<NumberServiceModel> service, IMapper mapper, ILog log)
        {
            this.service = service;
            this.mapper = mapper;
            this.log = log;
        }

        // GET: api/Numbers
        [HttpGet]
        public IEnumerable<NumberControllerModel> GetNumber()
        {
            try
            {
                log.Info("Reached GetNumbers() in NumbersController.cs");
                return service.GetAll().Select(x => mapper.Map<NumberControllerModel>(x));
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetNumbers() in NumbersController.cs", e));
                return null;
            }
        }

        // GET: api/Numbers/5
        [HttpGet("{id}")]
        public IActionResult GetNumber(int id)
        {
            try
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
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetNumber(int id) in NumbersController.cs",e));
                return NotFound();
            }
        }

        // PUT: api/Numbers/5
        [HttpPut("{id}")]
        public IActionResult PutNumber(int id, NumberControllerModel number)
        {
            try
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

                bool exists = service.Update(mapper.Map<NumberServiceModel>(number));

                if (exists)
                {
                    log.Info("Modified Number object in PutNumber(int id, NumberControllerModel number) in NumbersController.cs");
                    return NoContent();
                }

                log.Error("Number object with given id doesn't exist! Error occured in PutNumber(int id, NumberControllerModel number) in NumbersController.cs");

                return NotFound();
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PutNumber(int id, NumberControllerModel number) in NumbersController.cs",e));
                return NotFound();
            }
        }

        // POST: api/Numbers
        [HttpPost]
        public IActionResult PostNumber([FromBody] NumberControllerModel number)
        {
            try
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
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PostNumber([FromBody] NumberControllerModel number) in NumbersController.cs", e));
                return NotFound();
            }
        }

        // DELETE: api/Numbers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNumber(int id)
        {
            try
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
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in DeleteNumber(int id) in NumbersController.cs", e));
                return NotFound();
            }
        }
    }
}