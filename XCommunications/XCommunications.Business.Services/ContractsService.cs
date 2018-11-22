using AutoMapper;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XCommunications.Data.Models;
using XCommunications.Business.Interfaces;
using XCommunications.Business.Models;
using XCommunications.Data.Interfaces;

namespace XCommunications.Business.Services
{
    public class ContractsService : IService<ContractServiceModel>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private ILog log;

        public ContractsService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.log = log;
        }

        public IEnumerable<ContractServiceModel> GetAll()
        {
            try
            {
                log.Info("Reached GetAll() in ContractsService.cs");
                return unitOfWork.ContractRepository.GetAll().Select(x => mapper.Map<ContractServiceModel>(x));
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in GetAll() in CustomersService.cs!", e));
                return null;
            }
        }

        public ContractServiceModel Get(int id)
        {
            log.Info("Reached Get(int id) in ContractsService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Get(int id) in ContractsService.cs isn't positive number");
                    return null;
                }

                Contract c = null;
                c = unitOfWork.ContractRepository.GetById(id);

                ContractServiceModel contract = mapper.Map<ContractServiceModel>(c);

                if (contract == null)
                {
                    log.Error("Got null object in Get(int id) in ContractsService.cs");
                    return null;
                }

                log.Info("Returned Contract object from Get(int id) in ContractsService.cs");

                return contract;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Get(int id) in ContractsService.cs!", e));
                return null;
            }
        }

        public bool Update(ContractServiceModel contract)
        {

            log.Info("Reached Update(ContractServiceModel contract) in ContractsService.cs");

            try
            {
                Contract c = null;
                c = mapper.Map<Contract>(contract);
                c.Date = DateTime.Now;
                unitOfWork.ContractRepository.Update(c);
                log.Info("Modified Contract object in Update(ContractServiceModel contract) in ContractsService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(contract.Id))
                {
                    log.Error("Contract object with given id doesn't exist in Update(ContractServiceModel contract) in ContractsService.cs");
                }

                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Update(ContractServiceModel contract) in ContractsService.cs", e));
                return false;
            }
        }

        public bool Add(ContractServiceModel contract)
        {
            log.Info("Reached Add(ContractServiceModel contract) in ContractsService.cs");

            try
            {
                Contract c = null;
                c = mapper.Map<Contract>(contract);
                c.Date = DateTime.Now;
                unitOfWork.ContractRepository.Add(c);
                unitOfWork.Commit();
                log.Info("Added new Contract object in Add(ContractServiceModel contract) in ContractsService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(ContractServiceModel contract) in ContractsService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Add(ContractServiceModel contract) in ContractsService.cs", e));
                return false;
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in ContractsService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Delete(int id) in ContractsService.cs isn't positive number");
                    return false;
                }

                Contract contract = null;
                contract = unitOfWork.ContractRepository.GetById(id);

                if (contract == null)
                {
                    log.Error("Got null object in Delete(int id) in ContractsService.cs");
                    return false;
                }

                unitOfWork.ContractRepository.Remove(contract);
                unitOfWork.Commit();
                log.Info("Deleted Contract object in Delete(int id) in ContractsService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in ContractsService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Delete(int id) in ContractsService.cs", e));
                return false;
            }
        }

        private bool Exists(int id)
        {
            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Exists(int id) in ContractsService.cs isn't positive number");
                    return false;
                }

                Contract contract = unitOfWork.ContractRepository.GetById(id);

                if (contract == null)
                {
                    log.Error("Got null object in Exists(int id) in ContractsService.cs");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Exists(int id) in ContractsService.cs", e));
                return false;
            }
        }
    }
}
