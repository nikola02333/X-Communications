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
    public class CustomersService : IService<CustomerServiceModel>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private ILog log ;

        public CustomersService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.log = log;
        }

        public IEnumerable<CustomerServiceModel> GetAll()
        {
            try
            {
                log.Info("Reached GetAll() in CustomersService.cs");
                return unitOfWork.CustomerRepository.GetAll().Select(x => mapper.Map<CustomerServiceModel>(x));
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in GetAll() in CustomersService.cs!", e));
                return null;
            }
        }

        public CustomerServiceModel Get(int id)
        {
            log.Info("Reached Get(int id) in CustomersService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Get(int id) in CustomersService.cs isn't positive number");
                    return null;
                }

                Customer c = null;
                c = unitOfWork.CustomerRepository.GetById(id);

                CustomerServiceModel customer = mapper.Map<CustomerServiceModel>(c);

                if (customer == null)
                {
                    log.Error("Got null object in Get(int id) in CustomersService.cs");
                    return null;
                }

                log.Info("Returned Customer object from Get(int id) in CustomersService.cs");

                return customer;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Get(int id) in CustomersService.cs!", e));
                return null;
            }
        }

        public bool Update(CustomerServiceModel customer)
        {
            log.Info("Reached Put(CustomerServiceModel customer) in CustomersService.cs");

            try
            {
                Customer c = null;
                c = mapper.Map<Customer>(customer);
                unitOfWork.CustomerRepository.Update(c);
                unitOfWork.Commit();
                log.Info("Modified Customer object in Update(CustomerServiceModel customer) in CustomersService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(customer.Id))
                {
                    log.Error("Customer object with given id doesn't exist in Update(CustomerServiceModel customer) in CustomersService.cs");
                }

                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Update(CustomerServiceModel customer) in CustomersService.cs", e));
                return false;
            }
        }

        public bool Add(CustomerServiceModel customer)
        {
            log.Info("Reached Add(CustomerServiceModel customer) in CustomersService.cs");

            try
            {
                Customer c = null;
                c = mapper.Map<Customer>(customer);
                unitOfWork.CustomerRepository.Add(c);
                unitOfWork.Commit();
                log.Info("Added new Customer object in Add(CustomerServiceModel customer) in CustomersService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(CustomerServiceModel customer) in CustomersService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Add(CustomerServiceModel customer) in CustomersService.cs", e));
                return false;
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in CustomersService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Delete(int id) in CustomersService.cs isn't positive number");
                    return false;
                }

                Customer customer = null;
                customer = unitOfWork.CustomerRepository.GetById(id);

                if (customer == null)
                {
                    log.Error("Got null object in Delete(int id) in CustomersService.cs");
                    return false;
                }

                unitOfWork.CustomerRepository.Remove(customer);

                Contract c = null;
                c = unitOfWork.ContractRepository.Where(s => s.CustomerId == customer.Id);

                if(c != null)
                {
                    unitOfWork.ContractRepository.Remove(c);
                }

                RegistratedUser u = null;
                u = unitOfWork.RegistratedRepository.Where(s => s.CustomerId == customer.Id);

                if(u != null)
                {
                    unitOfWork.RegistratedRepository.Remove(u);
                }
                
                unitOfWork.Commit();
                log.Info("Deleted Customer object in Delete(int id) in CustomersService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in CustomersService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Delete(int id) in CustomersService.cs", e));
                return false;
            }
        }

        private bool Exists(int id)
        {
            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Delete(int id) in CustomersService.cs isn't positive number");
                    return false;
                }

                Customer customer = unitOfWork.CustomerRepository.GetById(id);

                if (customer == null)
                {
                    log.Error("Got null object in Exists(int id) in CustomersService.cs");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Exists(int id) in CustomersService.cs", e));
                return false;
            }
        }
    }
}

