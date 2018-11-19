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
    public class WorkersService : IWorkersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public WorkersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<WorkerServiceModel> GetAll()
        {
            return unitOfWork.WorkerRepository.GetAll().Select(x => mapper.Map<WorkerServiceModel>(x));
        }

        public WorkerControllerModel Get(int id)
        {
            Worker w = null;
            w = context.Worker.Find(id);
            
            WorkerControllerModel worker = mapper.Map<WorkerControllerModel>(w);

            if (worker == null)
            {
                return null;
            }

            return worker;
        }

        public bool Put(WorkerServiceModel worker)
        {
            Worker w = null;
            w = mapper.Map<Worker>(worker);

            try
            {
                context.Entry(w).State = EntityState.Modified;
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
            Worker w = null;
            w = mapper.Map<Worker>(worker);

            try
            {
                unitOfWork.WorkerRepository.Add(w);
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            Worker worker = null;
            worker = context.Worker.Find(id);

            try
            {
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
