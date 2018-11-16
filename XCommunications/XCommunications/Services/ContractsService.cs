using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommunications.Context;
using XCommunications.ModelsController;
using XCommunications.ModelsDB;
using XCommunications.ModelsService;
using XCommunications.Patterns.UnitOfWork;

namespace XCommunications.Services
{
    public class ContractsService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public IEnumerable<ContractServiceModel> GetAll()
        {
            return unitOfWork.ContractRepository.GetAll().Select(x => mapper.Map<ContractServiceModel>(x));
        }

        public ContractControllerModel Get(int id)
        {
            ContractControllerModel contract = mapper.Map< ContractControllerModel>(unitOfWork.ContractRepository.Get(mapper.Map<Contract>((id))));

            if (contract == null)
            {
                return null;
            }

            return contract;
        }

        public bool Put(ContractServiceModel contract)
        {
            try
            {
                context.Entry(contract).State = EntityState.Modified;
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

        public void Add(ContractServiceModel contract)
        {
            try
            {
                contract.Date = DateTime.Now;
                unitOfWork.ContractRepository.Add(mapper.Map<Contract>(contract));
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            Contract contract = mapper.Map<Contract>(unitOfWork.ContractRepository.Get(mapper.Map<Contract>(id)));

            try
            {
                if (contract == null)
                {
                    return false;
                }

                context.Contract.Remove(contract);
                context.Customer.RemoveRange(context.Customer.Where(s => s.Id == contract.CustomerId));
                context.Worker.RemoveRange(context.Worker.Where(s => s.Id == contract.WorkerId));
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
