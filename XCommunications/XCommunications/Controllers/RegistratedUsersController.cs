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
    public class RegistratedUsersController : ControllerBase
    {
        private IMapper mapper;
        private IRegistratedUsersService service;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RegistratedUsersController(IRegistratedUsersService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/RegistratedUsers
        [HttpGet]
        public IEnumerable<RegistratedUserControllerModel> GetRegistrated()
        {
            log.Info("Reached GetRegistrated() in RegistratedUsersController.cs");

            return service.GetAll().Select(x => mapper.Map<RegistratedUserControllerModel>(x));
        }

        // GET: api/RegistratedUsers/5
        [HttpGet("{id}")]
        public IActionResult GetRegistrated(int id)
        {
            log.Info("Reached GetRegistrated(int id) in RegistratedUsersController.cs");

            RegistratedUserControllerModel user = mapper.Map<RegistratedUserControllerModel>(service.Get(id));

            if (user == null)
            {
                log.Error("Got null object in GetRegistrated(int id) in RegistratedUsersController.cs");
                return NotFound();
            }

            log.Info("Returned RegistratedUser object from GetRegistrated(int id) in RegistratedUsersController.cs");

            return Ok(user);
        }

        // PUT: api/RegistratedUsers/5
        [HttpPut("{id}")]
        public IActionResult PutRegistrated(int id, RegistratedUserControllerModel user)
        {
            log.Info("Reached PutRegistrated(int id, RegistratedUserControllerModel user) in RegistratedUsersController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PutRegistrated(int id, RegistratedUserControllerModel user) in RegistratedUsersController.cs");
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                log.Error("RegistratedUser object isn't matched with given id! Error occured in PutRegistrated(int id, RegistratedUserControllerModel user) in RegistratedUsersController.cs");
                return BadRequest();
            }

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<RegistratedUserServiceModel>(user)));

            if (exists)
            {
                log.Info("Modified RegistratedUser object in PutRegistrated(int id, RegistratedUserControllerModel user) in RegistratedUsersController.cs");
                return NoContent();
            }

            log.Error("RegistratedUser object with given id doesn't exist! Error occured in PutRegistrated(int id, RegistratedUserControllerModel user) in RegistratedUsersController.cs");

            return NotFound();
        }

        // POST: api/RegistratedUsers
        [HttpPost]
        public IActionResult PostRegistrated([FromBody] RegistratedUserControllerModel user)
        {
            log.Info("Reached PostRegistrated([FromBody] RegistratedUserControllerModel user) in RegistratedUsersController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PostRegistrated([FromBody] RegistratedUserControllerModel user) in RegistratedUsersController.cs");
                return BadRequest(ModelState);
            }

            service.Add(mapper.Map<RegistratedUserServiceModel>(user));
            log.Info("Added new RegistratedUser object in PostRegistrated([FromBody] RegistratedUserControllerModel user) in RegistratedUsersController.cs");

            return NoContent();
        }

        // DELETE: api/RegistratedUsers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRegistrated(int id)
        {
            log.Info("Reached DeleteNumber(int id) in RegistratedUsersController.cs");

            if (!service.Delete(id))
            {
                log.Error("Got null object in DeleteRegistrated(int id) in RegistratedUsersController.cs");
                return NotFound();
            }

            log.Info("Deleted RegistratedUser object in DeleteRegistrated(int id) in RegistratedUsersController.cs");

            return NoContent();
        }
    }
}