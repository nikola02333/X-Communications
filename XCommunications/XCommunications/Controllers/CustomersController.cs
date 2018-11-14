using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XCommunications.Models;
using XCommunications.Patterns.UnitOfWork;

namespace XCommunications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public CustomersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return unitOfWork.CustomerRepository.GetAll();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            Customer customer = context.Customer.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            context.Entry(customer).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public IActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.CustomerRepository.Add(customer);
            unitOfWork.Commit();

            return CreatedAtRoute(new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            Customer customer = context.Customer.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            context.Customer.Remove(customer);
            context.Contract.RemoveRange(context.Contract.Where(s => s.CustomerId == customer.Id));
            context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.IdentificationCard == customer.Id));

            context.SaveChanges();

            return Ok(customer);
        }

        private bool CustomerExists(int id)
        {
            return context.Customer.Count(w => w.Id == id) > 0;
        }
    }
}