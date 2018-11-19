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

        public RegistratedUsersController(IRegistratedUsersService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/RegistratedUsers
        [HttpGet]
        public IEnumerable<RegistratedUserControllerModel> GetRegistrated()
        {
            return service.GetAll().Select(x => mapper.Map<RegistratedUserControllerModel>(x));
        }

        // GET: api/RegistratedUsers/5
        [HttpGet("{id}")]
        public IActionResult GetRegistrated(int id)
        {
            RegistratedUserControllerModel user = mapper.Map<RegistratedUserControllerModel>(service.Get(id));


            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/RegistratedUsers/5
        [HttpPut("{id}")]
        public IActionResult PutRegistrated(int id, RegistratedUserControllerModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<RegistratedUserServiceModel>(user)));

            if (exists)
            {
                return NoContent();
            }

            return NotFound();
        }

        // POST: api/RegistratedUsers
        [HttpPost]
        public IActionResult PostRegistrated([FromBody] RegistratedUserControllerModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.Add(mapper.Map<RegistratedUserServiceModel>(user));

            return NoContent();
        }

        // DELETE: api/RegistratedUsers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRegistrated(int id)
        {
            if (!service.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}