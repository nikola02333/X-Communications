﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XCommunications.ModelsDB;
using XCommunications.Patterns.Repository;

namespace XCommunications.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Worker> WorkerRepository { get; }
        IRepository<Number> NumberRepository { get; }
        IRepository<Simcard> SimcardRepository { get; }
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Contract> ContractRepository { get; }
        IRepository<RegistratedUser> RegistratedRepository { get; }

        // commit all changes
        void Commit();

        // discard all changes
        void Discard();

        void Dispose();
    }
}