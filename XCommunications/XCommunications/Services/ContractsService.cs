using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommunications.Context;
using XCommunications.Interfaces;
using XCommunications.ModelsController;
using XCommunications.ModelsDB;
using XCommunications.ModelsService;
using XCommunications.Patterns.UnitOfWork;

namespace XCommunications.Services
{
    public class ContractsService : IContractsService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ContractsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<ContractServiceModel> GetAll()
        {
            log.Info("Reached GetAll() in ContractsService.cs");

            return unitOfWork.ContractRepository.GetAll().Select(x => mapper.Map<ContractServiceModel>(x));
        }

        public ContractControllerModel Get(int id)
        {
            log.Info("Reached Get(int id) in ContractsService.cs");

            Contract c = null;
            c = context.Contract.Find(id);

            ContractControllerModel contract = mapper.Map<ContractControllerModel>(c);

            if (contract == null)
            {
                log.Error("Got null object in Get(int id) in ContractsService.cs");
                return null;
            }

            log.Info("Returned Contract object from Get(int id) in ContractsService.cs");

            return contract;
        }

        public bool Put(ContractServiceModel contract)
        {

            log.Info("Reached Put(ContractServiceModel contract) in ContractsService.cs");

            Contract c = null;
            c = mapper.Map<Contract>(contract);
            c.Date = DateTime.Now;

            try
            {
                context.Entry(c).State = EntityState.Modified;
                context.SaveChanges();
                log.Info("Modified Contract object in Put(ContractServiceModel contract) in ContractsService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(contract.Id))
                {
                    log.Error("Contract object with given id doesn't exist in Put(ContractServiceModel contract) in ContractsService.cs");
                }

                return false;

            }
        }

        public void Add(ContractServiceModel contract)
        {
            log.Info("Reached Add(ContractServiceModel contract) in ContractsService.cs");

            Contract c = null;
            c = mapper.Map<Contract>(contract);

            try
            {
                c.Date = DateTime.Now;
                unitOfWork.ContractRepository.Add(c);
                unitOfWork.Commit();
                log.Info("Added new Contract object in Add(ContractServiceModel contract) in ContractsService.cs");
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(ContractServiceModel contract) in ContractsService.cs");
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in ContractsService.cs");

            Contract contract = null;
            contract = context.Contract.Find(id);

            try
            {
                if (contract == null)
                {
                    log.Error("Got null object in Delete(int id) in ContractsService.cs");
                    return false;
                }

                context.Contract.Remove(contract);
                context.SaveChanges();
                log.Info("Deleted Contract object in Delete(int id) in ContractsService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(contract.Id))
                {
                    log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in ContractsService.cs");
                }

                return false;
            }
        }

        private bool Exists(int id)
        {
            return context.Contract.Count(w => w.Id == id) > 0;
        }
    }
}
