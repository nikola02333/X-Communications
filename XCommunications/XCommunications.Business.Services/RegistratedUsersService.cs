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
    public class RegistratedUsersService : IService<RegistratedUserServiceModel>
    {
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private ILog log;

        public RegistratedUsersService(IUnitOfWork unitOfWork, IMapper mapper, ILog log)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.log = log;
        }

        public IEnumerable<RegistratedUserServiceModel> GetAll()
        {
            try
            {
                log.Info("Reached GetAll() in RegistratedUsersService.cs");
                return unitOfWork.RegistratedRepository.GetAll().Select(x => mapper.Map<RegistratedUserServiceModel>(x));
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in GetAll() in RegistratedUsersService.cs!", e));
                return null;
            }
        }

        public RegistratedUserServiceModel Get(int id)
        {
            log.Info("Reached Get(int id) in RegistratedUsersService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Get(int id) in RegistratedUsersService.cs isn't positive number");
                    return null;
                }

                RegistratedUser r = null;
                r = unitOfWork.RegistratedRepository.GetById(id);
                RegistratedUserServiceModel user = mapper.Map<RegistratedUserServiceModel>(r);

                if (user == null)
                {
                    log.Error("Got null object in Get(int id) in RegistratedUsersService.cs");
                    return null;
                }

                log.Info("Returned RegistratedUser object from Get(int id) in RegistratedUsersService.cs");

                return user;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Get(int id) in RegistratedUsersService.cs!", e));
                return null;
            }
        }

        public bool Update(RegistratedUserServiceModel user)
        {
            log.Info("Reached Update(RegistratedUserServiceModel contract) in RegistratedUsersService.cs");

            try
            {
                RegistratedUser r = null;
                r = mapper.Map<RegistratedUser>(user);
                unitOfWork.RegistratedRepository.Update(r);
                log.Info("Modified RegistratedUser object in Update(RegistratedUserServiceModel user) in RegistratedUsersService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(user.Id))
                {
                    log.Error("RegistratedUser object with given id doesn't exist in Update(RegistratedUserServiceModel user) in RegistratedUsersService.cs");
                }

                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Update(RegistratedUserServiceModel user) in RegistratedUsersService.cs", e));
                return false;
            }
        }

        public bool Add(RegistratedUserServiceModel user)
        {
            log.Info("Reached Add(RegistratedUserServiceModel user) in RegistratedUsersService.cs");

            try
            {
                RegistratedUser r = null;
                r = mapper.Map<RegistratedUser>(user);
                unitOfWork.RegistratedRepository.Add(r);
                unitOfWork.Commit();
                log.Info("Added new RegistratedUser object in Add(RegistratedUserServiceModel user) in RegistratedUsersService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(RegistratedUserServiceModel user) in RegistratedUsersService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Add(RegistratedUserServiceModel user) in RegistratedUsersService.cs", e));
                return false;
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in RegistratedUsersService.cs");

            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Delete(int id) in RegistratedUsersService.cs isn't positive number");
                    return false;
                }

                RegistratedUser user = null;
                user = unitOfWork.RegistratedRepository.GetById(id);

                if (user == null)
                {
                    log.Error("Got null object in Delete(int id) in RegistratedUsersService.cs");
                    return false;
                }

                unitOfWork.RegistratedRepository.Remove(user);

                Simcard sc = null;
                sc = unitOfWork.SimcardRepository.Where(s => s.Imsi == user.Imsi);

                if(sc != null)
                {
                    unitOfWork.SimcardRepository.Remove(sc);
                }

                Number n = null;
                n = unitOfWork.NumberRepository.Where(s => s.Id == user.NumberId);

                if(n != null)
                {
                    unitOfWork.NumberRepository.Remove(n);
                }
                
                unitOfWork.Commit();
                log.Info("Deleted RegistratedUser object in Delete(int id) in RegistratedUsersService.cs");
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in RegistratedUsersService.cs");
                return false;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Delete(int id) in RegistratedUsersService.cs", e));
                return false;
            }
        }

        private bool Exists(int id)
        {
            try
            {
                if (id < 0)
                {
                    log.Error("Id that was given to Exists(int id) in RegistratedUsersService.cs isn't positive number");
                    return false;
                }

                RegistratedUser user = unitOfWork.RegistratedRepository.GetById(id);

                if (user == null)
                {
                    log.Error("Got null object in Exists(int id) in RegistratedUsersService.cs");
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                log.Error(String.Format("An exception {0} occured in Exists(int id) in RegistratedUsersService.cs", e));
                return false;
            }
        }
    }
}
