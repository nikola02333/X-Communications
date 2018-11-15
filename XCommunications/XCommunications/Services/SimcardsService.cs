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
    public class SimcardsService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public IEnumerable<SimcardServiceModel> GetAll()
        {
            return unitOfWork.SimcardRepository.GetAll();
        }

        public SimcardServiceModel Get(int id)
        {
            SimcardServiceModel sim = context.Simcard.Find(id);

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

        public bool Add(SimcardServiceModel sim)
        {
            try
            {
                unitOfWork.SimcardRepository.Add(sim);
                unitOfWork.Commit();
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

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                SimcardServiceModel sim = context.Simcard.Find(id);

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
            return context.Simcard.Count(w => w.Id == id) > 0;
        }
    }
}
