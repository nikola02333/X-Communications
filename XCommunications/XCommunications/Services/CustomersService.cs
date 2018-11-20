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
    public class CustomersService : ICustomersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CustomersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<CustomerServiceModel> GetAll()
        {
            log.Info("Reached GetAll() in CustomersService.cs");

            return unitOfWork.CustomerRepository.GetAll().Select(x => mapper.Map<CustomerServiceModel>(x));
        }

        public CustomerControllerModel Get(int id)
        {
            log.Info("Reached Get(int id) in CustomersService.cs");

            Customer c = null;
            c = context.Customer.Find(id);

            CustomerControllerModel customer = mapper.Map<CustomerControllerModel>(c);

            if (customer == null)
            {
                log.Error("Got null object in Get(int id) in CustomersService.cs");
                return null;
            }

            log.Info("Returned Customer object from Get(int id) in CustomersService.cs");

            return customer;
        }

        public bool Put(CustomerServiceModel customer)
        {
            log.Info("Reached Put(CustomerServiceModel customer) in CustomersService.cs");

            Customer c = null;
            c = mapper.Map<Customer>(customer);

            try
            {
                context.Entry(c).State = EntityState.Modified;
                context.SaveChanges();
                log.Info("Modified Customer object in Put(CustomerServiceModel customer) in CustomersService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(customer.Id))
                {
                    log.Error("Customer object with given id doesn't exist in Put(CustomerServiceModel customer) in CustomersService.cs");
                }

                return false;
            }
        }

        public void Add(CustomerServiceModel customer)
        {
            log.Info("Reached Add(CustomerServiceModel customer) in CustomersService.cs");

            Customer c = null;
            c = mapper.Map<Customer>(customer);

            try
            {
                unitOfWork.CustomerRepository.Add(c);
                unitOfWork.Commit();
                log.Info("Added new Customer object in Add(CustomerServiceModel customer) in CustomersService.cs");
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(CustomerServiceModel customer) in CustomersService.cs");
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in CustomersService.cs");

            Customer customer = null;
            customer = context.Customer.Find(id);

            try
            {
                if (customer == null)
                {
                    log.Error("Got null object in Delete(int id) in CustomersService.cs");
                    return false;
                }

                context.Customer.Remove(customer);
                context.Contract.RemoveRange(context.Contract.Where(s => s.CustomerId == customer.Id));
                context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.CustomerId == customer.Id));
                context.SaveChanges();
                log.Info("Deleted Customer object in Delete(int id) in CustomersService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(customer.Id))
                {
                    log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in CustomersService.cs");
                }

                return false;
            }
        }

        private bool Exists(int id)
        {
            return context.Customer.Count(w => w.Id == id) > 0;
        }
    }
}

