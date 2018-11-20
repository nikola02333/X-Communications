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
    public class RegistratedUsersService : IRegistratedUsersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RegistratedUsersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<RegistratedUserServiceModel> GetAll()
        {
            log.Info("Reached GetAll() in RegistratedUsersService.cs");

            return unitOfWork.RegistratedRepository.GetAll().Select(x => mapper.Map<RegistratedUserServiceModel>(x));
        }

        public RegistratedUserControllerModel Get(int id)
        {
            log.Info("Reached Get(int id) in RegistratedUsersService.cs");

            RegistratedUser r = null;
            r = context.RegistratedUser.Find(id);

            RegistratedUserControllerModel user = mapper.Map<RegistratedUserControllerModel>(r);

            if (user == null)
            {
                log.Error("Got null object in Get(int id) in RegistratedUsersService.cs");
                return null;
            }

            log.Info("Returned RegistratedUser object from Get(int id) in RegistratedUsersService.cs");

            return user;
        }

        public bool Put(RegistratedUserServiceModel user)
        {
            log.Info("Reached Put(RegistratedUserServiceModel contract) in RegistratedUsersService.cs");

            RegistratedUser r = null;
            r = mapper.Map<RegistratedUser>(user);

            try
            {
                context.Entry(r).State = EntityState.Modified;
                context.SaveChanges();
                log.Info("Modified RegistratedUser object in Put(RegistratedUserServiceModel user) in RegistratedUsersService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(user.Id))
                {
                    log.Error("RegistratedUser object with given id doesn't exist in Put(RegistratedUserServiceModel user) in RegistratedUsersService.cs");
                }

                return false;
            }
        }

        public void Add(RegistratedUserServiceModel user)
        {
            log.Info("Reached Add(RegistratedUserServiceModel user) in RegistratedUsersService.cs");

            RegistratedUser r = null;
            r = mapper.Map<RegistratedUser>(user);

            try
            {
                unitOfWork.RegistratedRepository.Add(mapper.Map<RegistratedUser>(r));
                unitOfWork.Commit();
                log.Info("Added new RegistratedUser object in Add(RegistratedUserServiceModel user) in RegistratedUsersService.cs");
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Add(RegistratedUserServiceModel user) in RegistratedUsersService.cs");
            }
        }

        public bool Delete(int id)
        {
            log.Info("Reached Delete(int id) in RegistratedUsersService.cs");

            RegistratedUser user = null;
            user = context.RegistratedUser.Find(id);

            try
            {
                if (user == null)
                {
                    log.Error("Got null object in Delete(int id) in RegistratedUsersService.cs");
                    return false;
                }

                context.RegistratedUser.Remove(user);
                context.Simcard.RemoveRange(context.Simcard.Where(s => s.Imsi == user.Imsi));
                context.Contract.RemoveRange(context.Contract.Where(s => s.CustomerId == user.CustomerId));
                context.Contract.RemoveRange(context.Contract.Where(s => s.WorkerId == user.WorkerId));
                context.Number.RemoveRange(context.Number.Where(s => s.Id == user.NumberId));
                context.SaveChanges();
                log.Info("Deleted RegistratedUser object in Delete(int id) in RegistratedUsersService.cs");

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                log.Error("A DbUpdateConcurrencyException occured in Delete(int id) in RegistratedUsersService.cs");
                return false;
            }
        }

        private bool Exists(int id)
        {
            return context.RegistratedUser.Count(w => w.Id == id) > 0;
        }
    }
}
