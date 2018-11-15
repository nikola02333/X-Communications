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
    public class NumbersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public IEnumerable<NumberServiceModel> GetAll()
        {
            return unitOfWork.NumberRepository.GetAll();
        }

        public NumberServiceModel Get(int id)
        {
            NumberServiceModel number = context.Number.Find(id);

            if (number == null)
            {
                return null;
            }

            return number;
        }

        public bool Put(NumberServiceModel number)
        {
            try
            {
                context.Entry(number).State = EntityState.Modified;
                context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(number.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public bool Add(NumberServiceModel number)
        {
            try
            {
                unitOfWork.NumberRepository.Add(number);
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(number.Id))
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
                NumberServiceModel number = context.Number.Find(id);

                if (number == null)
                {
                    return false;
                }

                context.Number.Remove(number);
                context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.NumberId == number.Id));
                context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(number.Id))
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
            return context.Number.Count(w => w.Id == id) > 0;
        }
    }
}
