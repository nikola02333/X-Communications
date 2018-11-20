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
    public class SimcardsService : ISimcardsService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SimcardsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<SimcardServiceModel> GetAll()
        {
            log.Info("Reached GetAll() in SimcardsService.cs");

            return unitOfWork.SimcardRepository.GetAll().Select(x => mapper.Map<SimcardServiceModel>(x));
        }

        public SimcardControllerModel Get(int id)
        {
            log.Info("Reached Get(int id) in SimcardsService.cs");

            Simcard s = null;
            s = context.Simcard.Find(id);

            SimcardControllerModel sim = mapper.Map<SimcardControllerModel>(s);

            if (sim == null)
            {
                log.Error("Got null object in Get(int id) in SimcardsService.cs");
                return null;
            }

            log.Info("Returned Simcard object from Get(int id) in SimcardsService.cs");

            return sim;
        }

        public bool Put(SimcardServiceModel sim)
        {
            log.Info("Reached Put(SimcardServiceModel sim) in SimcardsService.cs");

            Simcard s = null;
            s = mapper.Map<Simcard>(sim);

            try
            {
                context.Entry(s).State = EntityState.Modified;
                context.SaveChanges();
                log.Info("Modified Simcard object in Put(SimcardServiceModel sim) in SimcardsService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(sim.Imsi))
                {
                    log.Error("Simcard object with given id doesn't exist in Put(SimcardServiceModel sim) in SimcardsService.cs");
                }

                return false;
            }
        }

        public void Add(SimcardServiceModel sim)
        {
            log.Info("Reached Add(SimcardsServiceModel sim) in SimcardsService.cs");

            Simcard s = null;
            s = mapper.Map<Simcard>(sim);
            s.Status = true;

            try
            {
                unitOfWork.SimcardRepository.Add(s);
                unitOfWork.Commit();
                log.Info("Added new Simcard object in Add(SimcardServiceModel sim) in SimcardsService.cs");
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(SimcardServiceModel sim) in SimcardsService.cs");
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in SimcardsService.cs");

            Simcard sim = null;
            sim = context.Simcard.Find(id);

            try
            {
                if (sim == null)
                {
                    log.Error("Got null object in Delete(int id) in SimcardsService.cs");
                    return false;
                }

                context.Simcard.Remove(sim);
                context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.Imsi == sim.Imsi));
                context.SaveChanges();
                log.Info("Deleted Simcard object in Delete(int id) in SimcardsService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in SimcardsService.cs");
                return false;
            }
        }

        private bool Exists(int id)
        {
            return context.Simcard.Count(w => w.Imsi == id) > 0;
        }
    }
}
