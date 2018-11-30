using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XCommunications.Business.Interfaces;
using XCommunications.Business.Models;
using XCommunications.WebAPI.Models;

namespace XCommunications.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
  
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IMapper _mapper;
        private IService<CustomerServiceModel> _service;
        private ILog _log;

        public CustomersController(IService<CustomerServiceModel> service, IMapper mapper, ILog log)
        {
            this._service = service;
            this._mapper = mapper;
            this._log = log;
        }


        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerControllerModel> GetCustomer()
        {
            try
            {
                _log.Info("Reached GetCustomers() in CustomersController.cs");
                return _service.GetAll().Select(x => _mapper.Map<CustomerControllerModel>(x));
            }
            catch(Exception e)
            {
                _log.Error(string.Format("An exception {0} occured in GetCustomer() in CustomersController.cs", e));
                return null;
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            try
            {
                _log.Info("Reached GetCustomer(int id) in CustomersController.cs");

                
                CustomerControllerModel customer = _mapper.Map<CustomerControllerModel>(_service.Get(id));

                if (customer == null)
                {
                    _log.Error("Got null object in GetCustomer(int id) in CustomersController.cs");
                    return NotFound();
                }

                _log.Info("Returned Customer object from GetCustomer(int id) in CustomersController.cs");

                return Ok(customer);
            }
            catch (Exception e)
            {
                _log.Error(string.Format("An exception {0} occured in GetCustomer(int id) in CustomersController.cs", e));
                return StatusCode(500); //  internalError
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, CustomerControllerModel customer)
        {
            try
            {
                _log.Info("Reached PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");

                if (!ModelState.IsValid)
                {
                    _log.Error("A ModelState isn't valid error occured in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");
                    return BadRequest(); // NotFound
                }

                if (id != customer.Id)
                {
                    _log.Error("Customer object isn't matched with given id! Error occured in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");
                    return NotFound();
                }
                bool exists = _service.Update(_mapper.Map<CustomerServiceModel>(customer));

                if (exists)
                {
                    _log.Info("Modified Customer object in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");
                    return Ok(customer);
                }

                _log.Error("Customer object with given id doesn't exist! Error occured in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs");

                return NotFound();
            }
            catch (Exception e)
            {
                _log.Error(string.Format("An exception {0} occured in PutCustomer(int id, CustomerControllerModel customer) in CustomersController.cs", e));
                return StatusCode(500);
            }
        }

        // POST: api/Customers
        [HttpPost]
        public IActionResult PostCustomer([FromBody] CustomerControllerModel customer)
        {
            try
            {
             
                _log.Info("Reached PostCustomer([FromBody] CustomerControllerModel customer) in CustomersController.cs");
               
           if (!ModelState.IsValid)
                {
                    _log.Error("A ModelState isn't valid error occured in PostCustomer([FromBody] CustomerControllerModel customer) in CustomersController.cs");
                    return StatusCode(400);
                }
                
                _service.Add(_mapper.Map<CustomerServiceModel>(customer));
                _log.Info("Added new Customer object in PostCustomer([FromBody] CustomerControllerModel customer) in CustomersController.cs");

                return Ok(customer);
            }
            catch (Exception e)
            {
                _log.Error(string.Format("An exception {0} occured in PostCustomer([FromBody] CustomerControllerModel customer) in CustomersController.cs", e));
                return StatusCode(500);
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                _log.Info("Reached DeleteCustomer(int id) in CustomersController.cs");
               
                if (!_service.Delete(id))
                {
                    _log.Error("Got null object in DeleteCustomer(int id) in CustomersController.cs");
                    return NotFound();
                
                }

                _log.Info("Deleted Customer object in DeleteCustomer(int id) in CustomersController.cs");

                return Ok();
            }
            catch (Exception e)
            {
                _log.Error(string.Format("An exception {0} occured in DeleteCustomer(int id) in CustomersController.cs", e));
                return StatusCode(500);
            }
        }
    }
}