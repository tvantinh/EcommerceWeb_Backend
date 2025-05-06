using Ecommerce_BE.Contract.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_BE.Contract.Repositories.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetGenericRepository<T>() where T : class;
        Task SaveAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollBack();
    }
}
