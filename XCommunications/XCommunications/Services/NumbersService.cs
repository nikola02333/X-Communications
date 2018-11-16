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
    public class NumbersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public IEnumerable<NumberServiceModel> GetAll()
        {
            return unitOfWork.NumberRepository.GetAll().Select(x => mapper.Map<NumberServiceModel>(x));
        }

        public NumberControllerModel Get(int id)
        {
            NumberControllerModel number = mapper.Map<NumberControllerModel>(unitOfWork.NumberRepository.Get(mapper.Map<Number>(id)));

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

        public void Add(NumberServiceModel number)
        {
            try
            {
                number.Status = true;
                unitOfWork.NumberRepository.Add(mapper.Map<Number>(number));
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                Number number = mapper.Map<Number>(unitOfWork.NumberRepository.Get(mapper.Map<Number>(id)));

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
                throw;
            }
        }

        private bool Exists(int id)
        {
            return context.Number.Count(w => w.Id == id) > 0;
        }
    }
}
