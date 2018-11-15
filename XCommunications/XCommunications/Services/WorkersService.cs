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
    public class WorkersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public IEnumerable<WorkerServiceModel> GetAll()
        {
            return unitOfWork.WorkerRepository.GetAll();
        }

        public WorkerServiceModel Get(int id)
        {
            WorkerServiceModel worker = context.Worker.Find(id);

            if (worker == null)
            {
                return null;
            }

            return worker;
        }

        public bool Put(WorkerServiceModel worker)
        {
            try
            {
                context.Entry(worker).State = EntityState.Modified;
                context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(worker.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public bool Add(WorkerServiceModel worker)
        {
            try
            {
                unitOfWork.WorkerRepository.Add(worker);
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(worker.Id))
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
                WorkerServiceModel worker = context.Worker.Find(id);

                if (worker == null)
                {
                    return false;
                }

                context.Worker.Remove(worker);
                context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.WorkerId == worker.Id));
                context.Contract.RemoveRange(context.Contract.Where(s => s.WorkerId == worker.Id));
                context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(worker.Id))
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
            return context.Worker.Count(w => w.Id == id) > 0;
        }
    }
}
