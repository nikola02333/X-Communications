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
    public class RegistratedUsersController : ControllerBase
    {
        private IMapper mapper;
        private IService<RegistratedUserServiceModel> service;
        private ILog log;

        public RegistratedUsersController(IService<RegistratedUserServiceModel> service, IMapper mapper, ILog log)
        {
            this.service = service;
            this.mapper = mapper;
            this.log = log;
        }

        // GET: api/RegistratedUsers
        [HttpGet]
        public IEnumerable<RegistratedUserControllerModel> GetRegistrated()
        {
            try
            {
                log.Info("Reached GetRegistrated() in RegistratedUsersController.cs");
                return service.GetAll().Select(x => mapper.Map<RegistratedUserControllerModel>(x));
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetRegistrated() in RegistratedUsersController.cs",e));
                return null;
            }
        }

        // GET: api/RegistratedUsers/5
        [HttpGet("{id}")]
        public IActionResult GetRegistrated(int id)
        {
            try
            {
                log.Info("Reached GetRegistrated(int id) in RegistratedUsersController.cs");

                RegistratedUserControllerModel user = mapper.Map<RegistratedUserControllerModel>(service.Get(id));

                if (user == null)
                {
                    log.Error("Got null object in GetRegistrated(int id) in RegistratedUsersController.cs");
                    return NotFound("RegistratedUser object not found");
                }

                log.Info("Returned RegistratedUser object from GetRegistrated(int id) in RegistratedUsersController.cs");

                return Ok(user);
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetRegistrated(int id) in RegistratedUsersController.cs",e));
                return StatusCode(500);
            }
        }

        // PUT: api/RegistratedUsers/5
        [HttpPut("{id}")]
        public IActionResult PutRegistrated(int id, RegistratedUserControllerModel user)
        {
            try
            {
                log.Info("Reached PutRegistrated(int id, RegistratedUserControllerModel user) in RegistratedUsersController.cs");

                if (!ModelState.IsValid)
                {
                    log.Error("A ModelState isn't valid error occured in PutRegistrated(int id, RegistratedUserControllerModel user) in RegistratedUsersController.cs");
                    return StatusCode(400);
                }

                if (id != user.Id)
                {
                    log.Error("RegistratedUser object isn't matched with given id! Error occured in PutRegistrated(int id, RegistratedUserControllerModel user) in RegistratedUsersController.cs");
                    return BadRequest();
                }

                bool exists = service.Update(mapper.Map<RegistratedUserServiceModel>(user));

                if (exists)
                {
                    log.Info("Modified RegistratedUser object in PutRegistrated(int id, RegistratedUserControllerModel user) in RegistratedUsersController.cs");
                    return Ok();
                }

                log.Error("RegistratedUser object with given id doesn't exist! Error occured in PutRegistrated(int id, RegistratedUserControllerModel user) in RegistratedUsersController.cs");

                return NotFound();
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PutRegistrated(int id, RegistratedUserControllerModel user) in RegistratedUsersController.cs",e));
                return StatusCode(500);
            }
        }

        // POST: api/RegistratedUsers
        [HttpPost]
        public IActionResult PostRegistrated([FromBody] RegistratedUserControllerModel user)
        {
            try
            {
                log.Info("Reached PostRegistrated([FromBody] RegistratedUserControllerModel user) in RegistratedUsersController.cs");

                if (!ModelState.IsValid)
                {
                    log.Error("A ModelState isn't valid error occured in PostRegistrated([FromBody] RegistratedUserControllerModel user) in RegistratedUsersController.cs");
                    return StatusCode(400);
                }

                service.Add(mapper.Map<RegistratedUserServiceModel>(user));
                log.Info("Added new RegistratedUser object in PostRegistrated([FromBody] RegistratedUserControllerModel user) in RegistratedUsersController.cs");

                return Ok(user);
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PostRegistrated([FromBody] RegistratedUserControllerModel user) in RegistratedUsersController.cs",e));
                return StatusCode(500);
            }
        }

        // DELETE: api/RegistratedUsers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRegistrated(int id)
        {
            try
            {
                log.Info("Reached DeleteNumber(int id) in RegistratedUsersController.cs");

                if (!service.Delete(id))
                {
                    log.Error("Got null object in DeleteRegistrated(int id) in RegistratedUsersController.cs");
                    return NotFound();
                }

                log.Info("Deleted RegistratedUser object in DeleteRegistrated(int id) in RegistratedUsersController.cs");

                return Ok();
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in DeleteRegistrated(int id) in RegistratedUsersController.cs", e));
                return StatusCode(500);
            }
        }
    }
}