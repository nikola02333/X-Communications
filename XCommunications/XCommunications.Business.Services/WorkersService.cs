using AutoMapper;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using XCommunications.Data.Models;
using XCommunications.Business.Interfaces;
using XCommunications.Business.Models;
using XCommunications.Data.Interfaces;

namespace XCommunications.Business.Services
{
    public class WorkersService : IService<WorkerServiceModel>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private ILog log;

        public WorkersService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.log = log;
        }

        public IEnumerable<WorkerServiceModel> GetAll()
        {
            try
            {
                log.Info("Reached GetAll() in WorkersService.cs");
                return unitOfWork.WorkerRepository.GetAll().Select(x => mapper.Map<WorkerServiceModel>(x));
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in GetAll() in WorkersService.cs!", e));
                return null;
            }
        }

        public WorkerServiceModel Get(int id)
        {
            log.Info("Reached Get(int id) in WorkersService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Get(int id) in WorkersService.cs isn't positive number");
                    return null;
                }

                Worker w = null;
                w = unitOfWork.WorkerRepository.GetById(id);
                WorkerServiceModel worker = mapper.Map<WorkerServiceModel>(w);

                if (worker == null)
                {
                    log.Error("Got null object in Get(int id) in WorkersService.cs");
                    return null;
                }

                log.Info("Returned Worker object from Get(int id) in WorkersService.cs");

                return worker;
            }
            catch(Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Get(int id) in WorkersService.cs!", e));
                return null;
            }
        }

        public bool Update(WorkerServiceModel worker)
        {
            log.Info("Reached Update(WorkerServiceModel sim) in WorkersService.cs");

            try
            {
                Worker w = null;
                w = mapper.Map<Worker>(worker);
                unitOfWork.WorkerRepository.Update(w);
                log.Info("Modified Worker object in Update(WorkerServiceModel worker) in WorkersService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(worker.Id))
                {
                    log.Error("Worker object with given id doesn't exist in Update(WorkerServiceModel worker) in WorkersService.cs");
                }

                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Update(WorkerServiceModel worker) in WorkersService.cs", e));
                return false;
            }
        }

        public bool Add(WorkerServiceModel worker)
        {
            log.Info("Reached Add(WorkerServiceModel worker) in WorkersService.cs");

            try
            {
                Worker w = null;
                w = mapper.Map<Worker>(worker);
                unitOfWork.WorkerRepository.Add(w);
                unitOfWork.Commit();
                log.Info("Added new Worker object in Add(WorkerServiceModel worker) in WorkersService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(WorkerServiceModel worker) in WorkersService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Add(WorkerServiceModel worker) in WorkersService.cs", e));
                return false;
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in WorkersService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Delete(int id) in WorkersService.cs isn't positive number");
                    return false;
                }

                Worker worker = null;
                worker = unitOfWork.WorkerRepository.GetById(id);

                if (worker == null)
                {
                    log.Error("Got null object in Delete(int id) in WorkersService.cs");
                    return false;
                }

                unitOfWork.WorkerRepository.Remove(worker);

                RegistratedUser u = null;
                u = unitOfWork.RegistratedRepository.Where(s => s.WorkerId == id);

                if(u != null)
                {
                    unitOfWork.RegistratedRepository.Remove(u);
                }

                Contract c = null;
                c = unitOfWork.ContractRepository.Where(s => s.WorkerId == id);

                if( c!= null)
                {
                    unitOfWork.ContractRepository.Remove(c);
                }
                
                unitOfWork.Commit();
                log.Info("Deleted Worker object in Delete(int id) in WorkersService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in WorkersService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Delete(int id) in WorkersService.cs", e));
                return false;
            }
        }

        private bool Exists(int id)
        {
            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Exists(int id) in WorkersService.cs isn't positive number");
                    return false;
                }

                Worker worker = unitOfWork.WorkerRepository.GetById(id);

                if (worker==null)
                {
                    log.Error("Got null object in Exists(int id) in WorkersService.cs");
                    return false;
                }

                return true;
            }
            catch(Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Exists(int id) in WorkersService.cs", e));
                return false;
            }
        }
    }
}
