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
    public class ContractsController : ControllerBase
    {
        private IMapper mapper;
        private IService<ContractServiceModel> service;
        private ILog log;

        public ContractsController(IService<ContractServiceModel> service, IMapper mapper, ILog log)
        {
            this.service = service;
            this.mapper = mapper;
            this.log = log;
        }

        // GET: api/Contracts
        [HttpGet]
        public IEnumerable<ContractControllerModel> GetContract()
        {
            try
            {
                log.Info("Reached GetContract() in ContractsController.cs");
                return service.GetAll().Select(x => mapper.Map<ContractControllerModel>(x));
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetContract() in ContractsController.cs", e));
                return null;
            }
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public IActionResult GetContract(int id)
        {
            try
            {
                log.Info("Reached GetContract(int id) in ContractsController.cs");

                ContractControllerModel contract = mapper.Map<ContractControllerModel>(service.Get(id));

                if (contract == null)
                {
                    log.Error("Got null object in GetContract(int id) in ContractsController.cs");
                    return NotFound("Contract not found");
                }

                log.Info("Returned Contract object from GetContract(int id) in ContractsController.cs");

                return Ok(contract);
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in GetContract() in ContractsController.cs", e));
                return StatusCode(500);
            }
        }

        // PUT: api/Contracts/5
        [HttpPut("{id}")]
        public IActionResult PutContract(int id, ContractControllerModel contract)
        {
            try
            {
                log.Info("Reached PutContract(int id, ContractControllerModel contract) in ContractsController.cs");

                if (!ModelState.IsValid)
                {
                    log.Error("A ModelState isn't valid error occured in PutContract(int id, [FromBody] ContractControllerModel contract) in ContractsController.cs");
                    return BadRequest();
                }

                if (id != contract.Id)
                {
                    log.Error("Contract object isn't matched with given id! Error occured in PutContract(int id, ContractControllerModel contract) in ContractsController.cs");
                    return NotFound();
                }


              
                bool exists = service.Update(mapper.Map<ContractServiceModel>(contract));

                if (exists)
                {
                    log.Info("Modified Contract object in PutContract(int id, ContractControllerModel contract) in ContractsController.cs");
                    return Ok();
                }

                log.Error("Contract object with given id doesn't exist! Error occured in PutContract(int id, ContractControllerModel contract) in ContractsController.cs");

                return NotFound();
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PutContract(int id, ContractControllerModel contract) in ContractsController.cs", e));
               return  StatusCode(500);
            }
        }

        // POST: api/Contracts
        [HttpPost]
        public IActionResult PostContract([FromBody] ContractControllerModel contract)
        {
            try
            {
                log.Info("Reached PostContract([FromBody] ContractControllerModel contract) in ContractsController.cs");

                if (!ModelState.IsValid)
                {
                    log.Error("A ModelState isn't valid error occured in PostContract([FromBody] ContractControllerModel contract) in ContractsController.cs");
                    return BadRequest(ModelState);
                }

                service.Add(mapper.Map<ContractServiceModel>(contract));
                log.Info("Added new Contract object in PostContract([FromBody] ContractControllerModel contract) in ContractsController.cs");

                return Ok(contract);
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in PostContract([FromBody] ContractControllerModel contract) in ContractsController.cs", e));
                return NotFound();
            }

    
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteContract(int id)
        {
            try
            {
                log.Info("Reached DeleteContract(int id) in ContractsController.cs");

                if (!service.Delete(id))
                {
                    log.Error("Got null object in DeleteContract(int id) in ContractsController.cs");
                    return NotFound();
                }

                log.Info("Deleted Contract object in DeleteContract(int id) in ContractsController.cs");

                return Ok();
            }
            catch (Exception e)
            {
                log.Error(string.Format("An exception {0} occured in DeleteContract(int id) in ContractsController.cs", e));
                return StatusCode(500);
            }
        }
    }
}