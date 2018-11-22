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
    public class CustomersController : ControllerBase
    {
        private IMapper mapper;
        private IService<CustomerServiceModel> service;
        private ILog log;

        public CustomersController(IService<CustomerServiceModel> service, IMapper mapper, ILog log)
        {
            this.service = service;
            this.mapper = mapper;
            this.log = log;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerControllerModel> GetCustomer()
        {
            try
            {
                log.Info("Reached GetCustomers() in CustomersController.cs");
                return service.GetAll().Select(x => mapper.Map<CustomerControllerModel>(x));
            }
            catch(Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetCustomer() in CustomersController.cs", e));
                return null;
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            try
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
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetCustomer(int id) in CustomersController.cs", e));
                return NotFound();
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, CustomerControllerModel customer)
        {
            try
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

                bool exists = service.Update(mapper.Map<CustomerServiceModel>(customer));

                if (exists)
                {
                    log.Info("Modified Customer object in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");
                    return NoContent();
                }

                log.Error("Customer object with given id doesn't exist! Error occured in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");

                return NotFound();
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs", e));
                return NotFound();
            }
        }

        // POST: api/Customers
        [HttpPost]
        public IActionResult PostCustomer([FromBody] CustomerControllerModel customer)
        {
            try
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
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PostCustomer([FromBody] CustomerControllerModel customer) in CustomersController.cs", e));
                return NotFound();
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
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
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in DeleteCustomer(int id) in CustomersController.cs", e));
                return NotFound();
            }
        }
    }
}