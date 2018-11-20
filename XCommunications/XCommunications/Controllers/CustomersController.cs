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
    public class CustomersController : ControllerBase
    {
        private IMapper mapper;
        private ICustomersService service;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CustomersController(ICustomersService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerControllerModel> GetCustomer()
        {
            log.Info("Reached GetCustomers() in CustomersController.cs");

            return service.GetAll().Select(x => mapper.Map<CustomerControllerModel>(x));
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            log.Info("Reached GetCustomer(int id) in CustomersController.cs");

            CustomerControllerModel customer = mapper.Map<CustomerControllerModel>(service.Get(id));  

            if (customer == null)
            {
                log.Error("Got null object in GetCustomer(int id) in CustomersController.cs");
                return NotFound();
            }

            log.Info("Returned Customer object from GetCustomer(int id) in CustomersController.cs");

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, CustomerControllerModel customer)
        {
            log.Info("Reached PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                log.Error("Customer object isn't matched with given id! Error occured in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");
                return BadRequest();
            }

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<CustomerServiceModel>(customer)));

            if (exists)
            {
                log.Info("Modified Customer object in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");
                return NoContent();
            }

            log.Error("Customer object with given id doesn't exist! Error occured in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");

            return NotFound();
        }

        // POST: api/Customers
        [HttpPost]
        public IActionResult PostCustomer([FromBody] CustomerControllerModel customer)
        {
            log.Info("Reached PostCustomer([FromBody] CustomerControllerModel customer) in CustomersController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PostCustomer([FromBody] CustomerControllerModel customer) in CustomersController.cs");
                return BadRequest(ModelState);
            }

            service.Add(mapper.Map<CustomerServiceModel>(customer));
            log.Info("Added new Customer object in PostCustomer([FromBody] CustomerControllerModel customer) in CustomersController.cs");

            return NoContent();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            log.Info("Reached DeleteCustomer(int id) in CustomersController.cs");

            if (!service.Delete(id))
            {
                log.Error("Got null object in DeleteCustomer(int id) in CustomersController.cs");
                return NotFound();
            }

            log.Info("Deleted Customer object in DeleteCustomer(int id) in CustomersController.cs");

            return NoContent();
        }
    }
}