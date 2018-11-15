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
    public class CustomersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public IEnumerable<CustomerServiceModel> GetAll()
        {
            return unitOfWork.CustomerRepository.GetAll();
        }

        public CustomerServiceModel Get(int id)
        {
            CustomerServiceModel customer = context.Customer.Find(id);

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

        public bool Add(CustomerServiceModel customer)
        {
            try
            {
                unitOfWork.CustomerRepository.Add(customer);
                unitOfWork.Commit();
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

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                CustomerServiceModel customer = context.Customer.Find(id);

                if (number == null)
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

