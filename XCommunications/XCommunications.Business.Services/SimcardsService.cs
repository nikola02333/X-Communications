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
using System.Linq.Expressions;

namespace XCommunications.Business.Services
{
    public class SimcardsService : IService<SimcardServiceModel>, IQuery<SimcardServiceModel>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private ILog log;

        public SimcardsService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.log = log;
        }

        public IEnumerable<SimcardServiceModel> GetAll()
        {
            try
            {
                log.Info("Reached GetAll() in SimcardsService.cs");
                return unitOfWork.SimcardRepository.GetAll().Select(x => mapper.Map<SimcardServiceModel>(x));
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in GetAll() in SimcardsService.cs!", e));
                return null;
            }
        }

        public SimcardServiceModel Get(int id)
        {
            log.Info("Reached Get(int id) in SimcardsService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Get(int id) in SimcardsService.cs isn't positive number");
                    return null;
                }

                Simcard s = null;
                s = unitOfWork.SimcardRepository.GetById(id);
                SimcardServiceModel sim = mapper.Map<SimcardServiceModel>(s);

                if (sim == null)
                {
                    log.Error("Got null object in Get(int id) in SimcardsService.cs");
                    return null;
                }

                log.Info("Returned Simcard object from Get(int id) in SimcardsService.cs");

                return sim;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Get(int id) in SimcardsService.cs!", e));
                return null;
            }
        }

        public bool Update(SimcardServiceModel sim)
        {
            log.Info("Reached Put(SimcardServiceModel sim) in SimcardsService.cs");

            try
            {
                Simcard s = null;
                s = mapper.Map<Simcard>(sim);
                unitOfWork.SimcardRepository.Update(s);
                log.Info("Modified Simcard object in Update(SimcardServiceModel sim) in SimcardsService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(sim.Imsi))
                {
                    log.Error("Simcard object with given id doesn't exist in Update(SimcardServiceModel sim) in SimcardsService.cs");
                }

                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Update(SimcardServiceModel sim) in SimcardsService.cs", e));
                return false;
            }
        }

        public bool Add(SimcardServiceModel sim)
        {
            log.Info("Reached Add(SimcardsServiceModel sim) in SimcardsService.cs");

            try
            {
                Simcard s = null;
                s = mapper.Map<Simcard>(sim);
                s.Status = true;
                unitOfWork.SimcardRepository.Add(s);
                unitOfWork.Commit();
                log.Info("Added new Simcard object in Add(SimcardServiceModel sim) in SimcardsService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(SimcardServiceModel sim) in SimcardsService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Add(SimcardServiceModel sim) in SimcardsService.cs", e));
                return false;
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in SimcardsService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Delete(int id) in SimcardsService.cs isn't positive number");
                    return false;
                }

                Simcard sim = null;
                sim = unitOfWork.SimcardRepository.GetById(id);

                if (sim == null)
                {
                    log.Error("Got null object in Delete(int id) in SimcardsService.cs");
                    return false;
                }

                unitOfWork.SimcardRepository.Remove(sim);

                RegistratedUser u = null;
                u = unitOfWork.RegistratedRepository.Where(s => s.Imsi == sim.Imsi);

                if(u != null)
                {
                    unitOfWork.RegistratedRepository.Remove(u);
                }
                
                unitOfWork.Commit();
                log.Info("Deleted Simcard object in Delete(int id) in SimcardsService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in SimcardsService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Delete(int id) in SimcardsService.cs", e));
                return false;
            }
        }

        private bool Exists(int id)
        {
            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Exists(int id) in SimcardsService.cs isn't positive number");
                    return false;
                }

                Simcard sim = unitOfWork.SimcardRepository.GetById(id);

                if (sim == null)
                {
                    log.Error("Got null object in Exists(int id) in SimcardsService.cs");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Exists(int id) in SimcardsService.cs", e));
                return false;
            }
        }

        public IEnumerable<SimcardServiceModel> FindAvailable()
        {
            try
            {
                log.Info("Reached FindAvailable() in SimcardsService.cs");
                IEnumerable<SimcardServiceModel> retVal =  unitOfWork.SimcardRepository.GetAll().Select(x => mapper.Map<SimcardServiceModel>(x));
                return retVal.Where(s => s.Status == true);
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in FindAvailable() in SimcardsService.cs!", e));
                return null;
            }
        }
    }
}
