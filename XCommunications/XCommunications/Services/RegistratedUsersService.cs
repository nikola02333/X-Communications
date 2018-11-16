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
    public class RegistratedUsersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;
        private IMapper mapper;

        public IEnumerable<RegistratedUserServiceModel> GetAll()
        {
            return unitOfWork.RegistratedRepository.GetAll().Select(x => mapper.Map<RegistratedUserServiceModel>(x));
        }

        public RegistratedUserControllerModel Get(int id)
        {
            RegistratedUserControllerModel user = mapper.Map<RegistratedUserControllerModel>(unitOfWork.RegistratedRepository.Get(mapper.Map<RegistratedUser>(id)));

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public bool Put(RegistratedUserServiceModel user)
        {
            try
            {
                context.Entry(user).State = EntityState.Modified;
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
                    throw;
                }
            }
        }

        public void Add(RegistratedUserServiceModel user)
        {
            try
            {
                unitOfWork.RegistratedRepository.Add(mapper.Map<RegistratedUser>(user));
                unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public bool Delete(int id)
        {
            RegistratedUser user = mapper.Map<RegistratedUser>(unitOfWork.RegistratedRepository.Get(mapper.Map<RegistratedUser>(id)));

            try
            {
                if (user == null)
                {
                    return false;
                }

                context.RegistratedUser.Remove(user);
                context.Simcard.RemoveRange(context.Simcard.Where(s => s.Imsi == user.Imsi));
                context.Customer.RemoveRange(context.Customer.Where(s => s.Id == user.CustomerId));
                context.Worker.RemoveRange(context.Worker.Where(s => s.Id == user.WorkerId));
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
