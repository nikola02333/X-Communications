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

        public RegistratedUsersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IEnumerable<RegistratedUserServiceModel> GetAll()
        {
            return unitOfWork.RegistratedRepository.GetAll().Select(x => mapper.Map<RegistratedUserServiceModel>(x));
        }

        public RegistratedUserControllerModel Get(int id)
        {
            RegistratedUser r = null;
            r = context.RegistratedUser.Find(id);

            RegistratedUserControllerModel user = mapper.Map<RegistratedUserControllerModel>(r);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public bool Put(RegistratedUserServiceModel user)
        {
            RegistratedUser r = null;
            r = mapper.Map<RegistratedUser>(user);

            try
            {
                context.Entry(r).State = EntityState.Modified;
                context.SaveChanges();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(user.Id))
                {
                    return false;
                }
                else
                {
                    throw;      // moze baciti internal error server
                }
            }
        }

        public void Add(RegistratedUserServiceModel user)
        {
            RegistratedUser r = null;
            r = mapper.Map<RegistratedUser>(user);

            try
            {
                unitOfWork.RegistratedRepository.Add(mapper.Map<RegistratedUser>(r));
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            RegistratedUser user = null;
            user = context.RegistratedUser.Find(id);

            try
            {
                if (user == null)
                {
                    return false;
                }

                context.RegistratedUser.Remove(user);
                context.Simcard.RemoveRange(context.Simcard.Where(s => s.Imsi == user.Imsi));
                context.Contract.RemoveRange(context.Contract.Where(s => s.CustomerId == user.CustomerId));
                context.Contract.RemoveRange(context.Contract.Where(s => s.WorkerId == user.WorkerId));
                context.Number.RemoveRange(context.Number.Where(s => s.Id == user.NumberId));
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
            return context.RegistratedUser.Count(w => w.Id == id) > 0;
        }
    }
}
