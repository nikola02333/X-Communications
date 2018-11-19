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

        public ContractsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<ContractServiceModel> GetAll()
        {
            return unitOfWork.ContractRepository.GetAll().Select(x => mapper.Map<ContractServiceModel>(x));
        }

        public ContractControllerModel Get(int id)
        {
            Contract c = null;
            c = context.Contract.Find(id);

            ContractControllerModel contract = mapper.Map<ContractControllerModel>(c);

            if (contract == null)
            {
                return null;
            }

            return contract;
        }

        public bool Put(ContractServiceModel contract)
        {
            Contract c = null;
            c = mapper.Map<Contract>(contract);

            try
            {
                context.Entry(c).State = EntityState.Modified;
                context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(contract.Id))
                {
                    return false;
                }
                else
                {
                    throw;      // moze baciti internal error server
                }
            }
        }

        public void Add(ContractServiceModel contract)
        {
            Contract c = null;
            c = mapper.Map<Contract>(contract);

            try
            {
                c.Date = DateTime.Now;
                unitOfWork.ContractRepository.Add(c);
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            Contract contract = null;
            contract = context.Contract.Find(id);

            try
            {
                if (contract == null)
                {
                    return false;
                }

                context.Contract.Remove(contract);
                context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(contract.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool Exists(int id)
        {
            return context.Contract.Count(w => w.Id == id) > 0;
        }
    }
}
