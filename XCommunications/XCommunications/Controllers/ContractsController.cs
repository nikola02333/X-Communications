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
    public class ContractsController : ControllerBase
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public ContractsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Contracts
        [HttpGet]
        public IEnumerable<Contract> GetContract()
        {
            return unitOfWork.ContractRepository.GetAll();
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public IActionResult GetContract(int id)
        {
            Contract contract = context.Contract.Find(id);

            if (contract == null)
            {
                return NotFound();
            }

            return Ok(contract);
        }

        // PUT: api/Contracts/5
        [HttpPut("{id}")]
        public IActionResult PutContract(int id, Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contract.Id)
            {
                return BadRequest();
            }

            context.Entry(contract).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
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

        // POST: api/Contracts
        [HttpPost]
        public IActionResult PostContract(Contract contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.ContractRepository.Add(contract);
            unitOfWork.Commit();

            return CreatedAtRoute(new { id = contract.Id }, contract);
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteContract(int id)
        {
            Contract contract = context.Contract.Find(id);

            if (contract == null)
            {
                return NotFound();
            }

            context.Contract.Remove(contract);
            context.Customer.RemoveRange(context.Customer.Where(s => s.Id == contract.CustomerId));
            context.Worker.RemoveRange(context.Worker.Where(s => s.Id == contract.WorkerId));

            context.SaveChanges();

            return Ok(contract);
        }

        private bool ContractExists(int id)
        {
            return context.Contract.Count(w => w.Id == id) > 0;
        }
    }
}