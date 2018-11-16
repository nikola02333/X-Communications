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
    public class SimcardsService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public IEnumerable<SimcardServiceModel> GetAll()
        {
            return unitOfWork.SimcardRepository.GetAll().Select(x => mapper.Map<SimcardServiceModel>(x));
        }

        public SimcardControllerModel Get(int id)
        {
            SimcardControllerModel sim = mapper.Map<SimcardControllerModel>(unitOfWork.SimcardRepository.Get(mapper.Map<Simcard>(id)));

            if (sim == null)
            {
                return null;
            }

            return sim;
        }

        public bool Put(SimcardServiceModel sim)
        {
             try
            {
                context.Entry(sim).State = EntityState.Modified;
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

        public void Add(SimcardServiceModel sim)
        {
            try
            {
                sim.Status = true;
                unitOfWork.SimcardRepository.Add(mapper.Map<Simcard>(sim));
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            Simcard sim = mapper.Map<Simcard>(unitOfWork.SimcardRepository.Get(mapper.Map<Simcard>(id)));

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
