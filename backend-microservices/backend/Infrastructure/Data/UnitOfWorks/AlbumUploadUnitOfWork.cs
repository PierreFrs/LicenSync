using Core.Interfaces.IRepositories;
using Core.Interfaces.IUnitOfWork;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.UnitOfWorks
{
    public class AlbumUploadUnitOfWork(ApplicationDbContext context) : IAlbumUploadUnitOfWork
    {
        private IDbContextTransaction? _transaction;

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            context.Dispose();
        }
    }
}
