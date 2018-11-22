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
    public class NumbersService : IService<NumberServiceModel>, IQuery<NumberServiceModel>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private ILog log;

        public NumbersService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.log = log;
        }

        public IEnumerable<NumberServiceModel> GetAll()
        {
            try
            {
                log.Info("Reached GetAll() in NumbersService.cs");
                return unitOfWork.NumberRepository.GetAll().Select(x => mapper.Map<NumberServiceModel>(x));
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in GetAll() in NumbersService.cs!", e));
                return null;
            }
        }

        public NumberServiceModel Get(int id)
        {
            log.Info("Reached Get(int id) in NumbersService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Get(int id) in NumbersService.cs isn't positive number");
                    return null;
                }

                Number n = null;
                n = unitOfWork.NumberRepository.GetById(id);
                NumberServiceModel number = mapper.Map<NumberServiceModel>(n);

                if (number == null)
                {
                    log.Error("Got null object in Get(int id) in NumbersService.cs");
                    return null;
                }

                log.Info("Returned Number object from Get(int id) in NumbersService.cs");

                return number;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Get(int id) in NumbersService.cs!", e));
                return null;
            }
        }

        public bool Update(NumberServiceModel number)
        {
            log.Info("Reached Put(NumberServiceModel number) in NumbersService.cs");

            try
            {
                Number n = null;
                n = mapper.Map<Number>(number);
                unitOfWork.NumberRepository.Update(n);
                log.Info("Modified Number object in Update(NumberServiceModel number) in NumbersService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(number.Id))
                {
                    log.Error("Number object with given id doesn't exist in Update(NumberServiceModel number) in NumbersService.cs");
                }

                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Update(NumberServiceModel number) in NumbersService.cs", e));
                return false;
            }
        }

        public bool Add(NumberServiceModel number)
        {
            log.Info("Reached Add(NumberServiceModel number) in NumbersService.cs");

            try
            {
                Number n = null;
                n = mapper.Map<Number>(number);
                n.Status = true;
                unitOfWork.NumberRepository.Add(n);
                unitOfWork.Commit();
                log.Info("Added new Number object in Add(NumberServiceModel number) in NumbersService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(NumberServiceModel number) in NumbersService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Add(NumberServiceModel number) in NumbersService.cs", e));
                return false;
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in NumbersService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Delete(int id) in NumbersService.cs isn't positive number");
                    return false;
                }

                Number number = null;
                number = unitOfWork.NumberRepository.GetById(id);

                if (number == null)
                {
                    log.Error("Got null object in Delete(int id) in NumbersService.cs");
                    return false;
                }

                unitOfWork.NumberRepository.Remove(number);

                RegistratedUser u = null;
                u = unitOfWork.RegistratedRepository.Where(s => s.NumberId == number.Id);

                if(u != null)
                {
                    unitOfWork.RegistratedRepository.Remove(u);
                }

                unitOfWork.Commit();
                log.Info("Deleted Number object in Delete(int id) in NumbersService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in NumbersService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Delete(int id) in NumbersService.cs", e));
                return false;
            }
        }

        private bool Exists(int id)
        {
            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Exists(int id) in NumbersService.cs isn't positive number");
                    return false;
                }

                Number number = unitOfWork.NumberRepository.GetById(id);

                if (number == null)
                {
                    log.Error("Got null object in Exists(int id) in NumbersService.cs");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Exists(int id) in NumbersService.cs", e));
                return false;
            }
        }

        public IEnumerable<NumberServiceModel> FindAvailable()
        {
            try
            {
                log.Info("Reached FindAvailable() in NumbersService.cs");
                IEnumerable<NumberServiceModel> retVal = unitOfWork.SimcardRepository.GetAll().Select(x => mapper.Map<NumberServiceModel>(x));
                return retVal.Where(s => s.Status == true);
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in FindAvailable() in NumbersService.cs!", e));
                return null;
            }
        }
    }
}
