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

        public SimcardsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<SimcardServiceModel> GetAll()
        {
            return unitOfWork.SimcardRepository.GetAll().Select(x => mapper.Map<SimcardServiceModel>(x));
        }

        public SimcardControllerModel Get(int id)
        {
            Simcard s = null;
            s = context.Simcard.Find(id);

            SimcardControllerModel sim = mapper.Map<SimcardControllerModel>(s);

            if (sim == null)
            {
                return null;
            }

            return sim;
        }

        public bool Put(SimcardServiceModel sim)
        {
            Simcard s = null;
            s = mapper.Map<Simcard>(sim);

            try
            {
                context.Entry(s).State = EntityState.Modified;
                context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(sim.Imsi))
                {
                    return false;
                }
                else
                {
                    throw;      // moze baciti internal error server
                }
            }
        }

        public void Add(SimcardServiceModel sim)
        {
            Simcard s = null;
            s = mapper.Map<Simcard>(sim);
            s.Status = true;

            try
            {
                unitOfWork.SimcardRepository.Add(s);
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            Simcard sim = null;
            sim = context.Simcard.Find(id);

            try
            {
                if (sim == null)
                {
                    return false;
                }

                context.Simcard.Remove(sim);
                context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.Imsi == sim.Imsi));
                context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(sim.Imsi))
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
            return context.Simcard.Count(w => w.Imsi == id) > 0;
        }
    }
}
