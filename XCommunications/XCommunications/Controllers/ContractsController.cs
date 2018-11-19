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
    public class ContractsController : ControllerBase
    {
        private IMapper mapper;
        private IContractsService service;

        public ContractsController(IContractsService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Contracts
        [HttpGet]
        public IEnumerable<ContractControllerModel> GetContract()
        {
            return service.GetAll().Select(x => mapper.Map<ContractControllerModel>(x));
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public IActionResult GetContract(int id)
        {
            ContractControllerModel contract = mapper.Map<ContractControllerModel>(service.Get(id));

            if (contract == null)
            {
                return NotFound();
            }

            return Ok(contract);
        }

        // PUT: api/Contracts/5
        [HttpPut("{id}")]
        public IActionResult PutContract(int id, ContractControllerModel contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // DOGOVORITI SE DA LI CEMO DA GENERISEMO ID ILI DA GA DODAJEMO !!!
            //if (id != contract.Id)
            //{
            //    return BadRequest();
            //}

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<ContractServiceModel>(contract)));

            if (exists)
            {
                return NoContent();
            }

            return NotFound();

        }

        // POST: api/Contracts
        [HttpPost]
        public IActionResult PostContract([FromBody] ContractControllerModel contract)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            service.Add(mapper.Map<ContractServiceModel>(contract));

            return NoContent();
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteContract(int id)
        {
            if(!service.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}