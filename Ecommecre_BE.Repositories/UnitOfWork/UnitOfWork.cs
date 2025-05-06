using Ecommecre_BE.Repositories.DataContext;
using Ecommerce_BE.Contract.Repositories.IUnitOfWork;
using Ecommerce_BE.Contract.Repositories.Repositories;
using System;
using System.Collections.Generic;

namespace Ecommecre_BE.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private bool _disposed = false;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollBack()
        {
            _context.Database.RollbackTransaction();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IGenericRepository<T> GetGenericRepository<T>() where T : class
        {
            var type = typeof(T);

            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new GenericRepository<T>(_context);
                _repositories[type] = repoInstance;
            }

            return (IGenericRepository<T>)_repositories[type]!;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _context.Dispose();

                _disposed = true;
            }
        }
    }
}
