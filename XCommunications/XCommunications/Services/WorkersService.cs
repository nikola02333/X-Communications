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
    public class WorkersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public IEnumerable<WorkerServiceModel> GetAll()
        {
            return unitOfWork.WorkerRepository.GetAll().Select(x => mapper.Map<WorkerServiceModel>(x));
        }

        public WorkerControllerModel Get(int id)
        {
            WorkerControllerModel worker = mapper.Map< WorkerControllerModel>(unitOfWork.WorkerRepository.Get(mapper.Map<Worker>(id)));

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
                if (!Exists(worker.Id))
                {
                    return false;
                }
                else
                {
                    throw;      // moze baciti internal error server
                }
            }
        }

        public void Add(WorkerServiceModel worker)
        {
            try
            {
                unitOfWork.WorkerRepository.Add(mapper.Map<Worker>(worker));
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
                Worker worker = mapper.Map<Worker>(unitOfWork.WorkerRepository.Get(mapper.Map<Worker>(id)));

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
                throw;
            }
        }

        private bool Exists(int id)
        {
            return context.Worker.Count(w => w.Id == id) > 0;
        }
    }
}
