using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommunications.Context;
using XCommunications.Interfaces;
using XCommunications.ModelsDB;
using XCommunications.Patterns.Repository;

namespace XCommunications.Patterns.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly XCommunicationsContext dbContext;

        public IRepository<Worker> WorkerRepository => new Repository<Worker>(dbContext);
        public IRepository<Number> NumberRepository => new Repository<Number>(dbContext);
        public IRepository<Simcard> SimcardRepository => new Repository<Simcard>(dbContext);
        public IRepository<Customer> CustomerRepository => new Repository<Customer>(dbContext);
        public IRepository<Contract> ContractRepository => new Repository<Contract>(dbContext);
        public IRepository<RegistratedUser> RegistratedRepository => new Repository<RegistratedUser>(dbContext);

        public UnitOfWork(XCommunicationsContext context)
        {
            dbContext = context;
        }

        public void Commit()
        {
            dbContext.SaveChanges();
        }

        public void Discard()
        {
            foreach (var entity in dbContext.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entity.Reload();
                        break;
                }
            }
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
