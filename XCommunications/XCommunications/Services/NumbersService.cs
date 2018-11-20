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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NumbersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<NumberServiceModel> GetAll()
        {
            log.Info("Reached GetAll() in NumbersService.cs");

            return unitOfWork.NumberRepository.GetAll().Select(x => mapper.Map<NumberServiceModel>(x));
        }

        public NumberControllerModel Get(int id)
        {
            log.Info("Reached Get(int id) in NumbersService.cs");

            Number n = null;
            n = context.Number.Find(id);

            NumberControllerModel number = mapper.Map<NumberControllerModel>(n);

            if (number == null)
            {
                log.Error("Got null object in Get(int id) in NumbersService.cs");
                return null;
            }

            log.Info("Returned Number object from Get(int id) in NumbersService.cs");

            return number;
        }

        public bool Put(NumberServiceModel number)
        {
            log.Info("Reached Put(NumberServiceModel number) in NumbersService.cs");

            Number n = null;
            n = mapper.Map<Number>(number);

            try
            {
                context.Entry(n).State = EntityState.Modified;
                context.SaveChanges();
                log.Info("Modified Number object in Put(NumberServiceModel number) in NumbersService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(number.Id))
                {
                    log.Error("Number object with given id doesn't exist in Put(NumberServiceModel number) in NumbersService.cs");
                }

                return false;
            }
        }

        public void Add(NumberServiceModel number)
        {
            log.Info("Reached Add(NumberServiceModel number) in NumbersService.cs");

            Number n = null;
            n = mapper.Map<Number>(number);

            try
            {
                n.Status = true;
                unitOfWork.NumberRepository.Add(n);
                unitOfWork.Commit();
                log.Info("Added new Number object in Add(NumberServiceModel number) in NumbersService.cs");
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(NumberServiceModel number) in NumbersService.cs");
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in NumbersService.cs");

            Number number = null;
            number = context.Number.Find(id);

            try
            {
                if (number == null)
                {
                    log.Error("Got null object in Delete(int id) in NumbersService.cs");
                    return false;
                }

                context.Number.Remove(number);
                context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.NumberId == number.Id));
                context.SaveChanges();
                log.Info("Deleted Number object in Delete(int id) in NumbersService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in NumbersService.cs");
                return false;
            }
        }

        private bool Exists(int id)
        {
            return context.Number.Count(w => w.Id == id) > 0;
        }
    }
}
