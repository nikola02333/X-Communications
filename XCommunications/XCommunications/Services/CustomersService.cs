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
    public class CustomersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public IEnumerable<CustomerServiceModel> GetAll()
        {
            return unitOfWork.CustomerRepository.GetAll().Select(x => mapper.Map<CustomerServiceModel>(x));
        }

        public CustomerControllerModel Get(int id)
        {
            CustomerControllerModel customer = mapper.Map<CustomerControllerModel>(unitOfWork.CustomerRepository.Get(mapper.Map<Customer>(id)));

            if (customer == null)
            {
                return null;
            }

            return customer;
        }

        public bool Put(CustomerServiceModel customer)
        {
            try
            {
                context.Entry(customer).State = EntityState.Modified;
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

        public void Add(CustomerServiceModel customer)
        {
            try
            {
                unitOfWork.CustomerRepository.Add(mapper.Map<Customer>(customer));
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            Customer customer = mapper.Map<Customer>(unitOfWork.CustomerRepository.Get(mapper.Map<Customer>(id)));

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

