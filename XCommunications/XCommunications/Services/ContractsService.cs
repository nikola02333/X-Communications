using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommunications.Context;
using XCommunications.ModelsService;
using XCommunications.Patterns.UnitOfWork;

namespace XCommunications.Services
{
    public class ContractsService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public IEnumerable<ContractServiceModel> GetAll()
        {
            return unitOfWork.ContractRepository.GetAll();
        }

        public ContractServiceModel Get(int id)
        {
            ContractServiceModel contract = context.Contract.Find(id);

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

        public bool Add(ContractServiceModel contract)
        {
            try
            {
                unitOfWork.ContractRepository.Add(contract);
                unitOfWork.Commit();
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

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                ContractServiceModel contract = context.Contract.Find(id);

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
