using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XCommunications.Context;
using XCommunications.ModelsController;
using XCommunications.ModelsService;
using XCommunications.Patterns.UnitOfWork;
using XCommunications.Services;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IMapper mapper;
        private CustomersService service = new CustomersService();

        public CustomersController(CustomersService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerControllerModel> GetCustomers()
        {
            return service.GetAll().Select(x => mapper.Map<CustomerControllerModel>(x));
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            CustomerControllerModel customer = mapper.Map<CustomerControllerModel>(service.Get(id));  

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, CustomerControllerModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<CustomerServiceModel>(customer)));

            if (exists)
            {
                return NoContent();
            }

            return NotFound();
        }

        // POST: api/Customers
        [HttpPost]
        public IActionResult PostCustomer([FromBody] CustomerControllerModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           service.Add(mapper.Map<CustomerServiceModel>(customer));

            return CreatedAtRoute(new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (!service.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}