using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommunications.Context;
using XCommunications.ModelsService;
using XCommunications.Patterns.UnitOfWork;

namespace XCommunications.Services
{
    public class RegistratedUsersService
    {
        private XCommunicationsContext context = new XCommunicationsContext();
        private IUnitOfWork unitOfWork;

        public IEnumerable<RegistratedUserServiceModel> GetAll()
        {
            return unitOfWork.RegistratedRepository.GetAll();
        }

        public RegistratedUserServiceModel Get(int id)
        {
            RegistratedUserServiceModel user = context.RegistratedUser.Find(id);

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

        public bool Add(RegistratedUserServiceModel user)
        {
            try
            {
                unitOfWork.RegistratedUserRepository.Add(user);
                unitOfWork.Commit();
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

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                RegistratedUserServiceModel user = context.RegistratedUser.Find(id);

                if (user == null)
                {
                    return false;
                }

                ccontext.RegistratedUser.Remove(user);
                context.Simcard.RemoveRange(context.Simcard.Where(s => s.Imsi == user.Imsi));
                context.Customer.RemoveRange(context.Customer.Where(s => s.Id == user.CustomerId));
                context.Worker.RemoveRange(context.Worker.Where(s => s.Id == user.WorkerId));
                context.Number.RemoveRange(context.Number.Where(s => s.Id == user.NumberId));
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

        private bool Exists(int id)
        {
            return context.RegistratedUser.Count(w => w.Id == id) > 0;
        }
    }
}
