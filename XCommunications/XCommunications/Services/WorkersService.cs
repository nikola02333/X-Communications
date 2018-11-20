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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public WorkersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<WorkerServiceModel> GetAll()
        {
            log.Info("Reached GetAll() in WorkersService.cs");

            return unitOfWork.WorkerRepository.GetAll().Select(x => mapper.Map<WorkerServiceModel>(x));
        }

        public WorkerControllerModel Get(int id)
        {
            log.Info("Reached Get(int id) in WorkersService.cs");

            Worker w = null;
            w = context.Worker.Find(id);
            
            WorkerControllerModel worker = mapper.Map<WorkerControllerModel>(w);

            if (worker == null)
            {
                log.Error("Got null object in Get(int id) in WorkersService.cs");
                return null;
            }

            log.Info("Returned Worker object from Get(int id) in WorkersService.cs");

            return worker;
        }

        public bool Put(WorkerServiceModel worker)
        {
            log.Info("Reached Put(WorkerServiceModel sim) in WorkersService.cs");

            Worker w = null;
            w = mapper.Map<Worker>(worker);

            try
            {
                context.Entry(w).State = EntityState.Modified;
                context.SaveChanges();
                log.Info("Modified Worker object in Put(WorkerServiceModel worker) in WorkersService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(worker.Id))
                {
                    log.Error("Worker object with given id doesn't exist in Put(WorkerServiceModel worker) in WorkersService.cs");
                }

                return false;
            }
        }

        public void Add(WorkerServiceModel worker)
        {
            log.Info("Reached Add(WorkerServiceModel worker) in WorkersService.cs");

            Worker w = null;
            w = mapper.Map<Worker>(worker);

            try
            {
                unitOfWork.WorkerRepository.Add(w);
                unitOfWork.Commit();
                log.Info("Added new Worker object in Add(WorkerServiceModel worker) in WorkersService.cs");
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(WorkerServiceModel worker) in WorkersService.cs");
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in WorkersService.cs");

            Worker worker = null;
            worker = context.Worker.Find(id);

            try
            {
                if (worker == null)
                {
                    log.Error("Got null object in Delete(int id) in WorkersService.cs");
                    return false;
                }

                context.Worker.Remove(worker);
                context.RegistratedUser.RemoveRange(context.RegistratedUser.Where(s => s.WorkerId == worker.Id));
                context.Contract.RemoveRange(context.Contract.Where(s => s.WorkerId == worker.Id));
                context.SaveChanges();
                log.Info("Deleted Worker object in Delete(int id) in WorkersService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in WorkersService.cs");
                return false;
            }
        }

        private bool Exists(int id)
        {
            return context.Worker.Count(w => w.Id == id) > 0;
        }
    }
}
