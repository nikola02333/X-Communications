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

        public CustomersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<CustomerServiceModel> GetAll()
        {
            return unitOfWork.CustomerRepository.GetAll().Select(x => mapper.Map<CustomerServiceModel>(x));
        }

        public CustomerControllerModel Get(int id)
        {
            Customer c = null;
            c = context.Customer.Find(id);

            CustomerControllerModel customer = mapper.Map<CustomerControllerModel>(c);

            if (customer == null)
            {
                return null;
            }

            return customer;
        }

        public bool Put(CustomerServiceModel customer)
        {
            Customer c = null;
            c = mapper.Map<Customer>(customer);

            try
            {
                context.Entry(c).State = EntityState.Modified;
                context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(customer.Id))
                {
                    return false;
                }
                else
                {
                    throw;      // moze baciti internal error server
                }
            }
        }

        public void Add(CustomerServiceModel customer)
        {
            Customer c = null;
            c = mapper.Map<Customer>(customer);

            try
            {
                unitOfWork.CustomerRepository.Add(c);
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            Customer customer = null;
            customer = context.Customer.Find(id);

            try
            {
                if (customer == null)
                {
                    return false;
                }

                context.Customer.Remove(customer);
                context.Contract.RemoveRange(context.Contract.Where(s => s.CustomerId == customer.Id));
                context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.CustomerId == customer.Id));
                context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(customer.Id))
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
            return context.Customer.Count(w => w.Id == id) > 0;
        }
    }
}

