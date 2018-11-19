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
    public class NumbersService : INumbersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public NumbersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<NumberServiceModel> GetAll()
        {
            return unitOfWork.NumberRepository.GetAll().Select(x => mapper.Map<NumberServiceModel>(x));
        }

        public NumberControllerModel Get(int id)
        {
            Number n = null;
            n = context.Number.Find(id);

            NumberControllerModel number = mapper.Map<NumberControllerModel>(n);

            if (number == null)
            {
                return null;
            }

            return number;
        }

        public bool Put(NumberServiceModel number)
        {
            Number n = null;
            n = mapper.Map<Number>(number);

            try
            {
                context.Entry(n).State = EntityState.Modified;
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
                    throw;          // moze baciti internal error server
                }
            }
        }

        public void Add(NumberServiceModel number)
        {
            Number n = null;
            n = mapper.Map<Number>(number);

            try
            {
                n.Status = true;
                unitOfWork.NumberRepository.Add(n);
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            Number number = null;
            number = context.Number.Find(id);

            try
            {
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
