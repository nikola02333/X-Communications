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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ContractsController(IContractsService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/Contracts
        [HttpGet]
        public IEnumerable<ContractControllerModel> GetContract()
        {
            log.Info("Reached GetContract() in ContractsController.cs");

            return service.GetAll().Select(x => mapper.Map<ContractControllerModel>(x));
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public IActionResult GetContract(int id)
        {
            log.Info("Reached GetContract(int id) in ContractsController.cs");

            ContractControllerModel contract = mapper.Map<ContractControllerModel>(service.Get(id));

            if (contract == null)
            {
                log.Error("Got null object in GetContract(int id) in ContractsController.cs");
                return NotFound();
            }

            log.Info("Returned Contract object from GetContract(int id) in ContractsController.cs");

            return Ok(contract);
        }

        // PUT: api/Contracts/5
        [HttpPut("{id}")]
        public IActionResult PutContract(int id, ContractControllerModel contract)
        {
            log.Info("Reached PutContract(int id, ContractControllerModel contract) in ContractsController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PutContract(int id, [FromBody] ContractControllerModel contract) in ContractsController.cs");
                return BadRequest(ModelState);
            }

            if (id != contract.Id)
            {
                log.Error("Contract object isn't matched with given id! Error occured in PutContract(int id, ContractControllerModel contract) in ContractsController.cs");
                return BadRequest();
            }

            bool exists = mapper.Map<bool>(service.Put(mapper.Map<ContractServiceModel>(contract)));

            if (exists)
            {
                log.Info("Modified Contract object in PutContract(int id, ContractControllerModel contract) in ContractsController.cs");
                return NoContent();
            }

            log.Error("Contract object with given id doesn't exist! Error occured in PutContract(int id, ContractControllerModel contract) in ContractsController.cs");

            return NotFound();

        }

        // POST: api/Contracts
        [HttpPost]
        public IActionResult PostContract([FromBody] ContractControllerModel contract)
        {
            log.Info("Reached PostContract([FromBody] ContractControllerModel contract) in ContractsController.cs");

            if (!ModelState.IsValid)
            {
                log.Error("A ModelState isn't valid error occured in PostContract([FromBody] ContractControllerModel contract) in ContractsController.cs");
                return BadRequest(ModelState);
            }

            service.Add(mapper.Map<ContractServiceModel>(contract));
            log.Info("Added new Contract object in PostContract([FromBody] ContractControllerModel contract) in ContractsController.cs");

            return NoContent();
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteContract(int id)
        {
            log.Info("Reached DeleteContract(int id) in ContractsController.cs");

            if (!service.Delete(id))
            {
                log.Error("Got null object in DeleteContract(int id) in ContractsController.cs");
                return NotFound();
            }

            log.Info("Deleted Contract object in DeleteContract(int id) in ContractsController.cs");

            return NoContent();
        }
    }
}