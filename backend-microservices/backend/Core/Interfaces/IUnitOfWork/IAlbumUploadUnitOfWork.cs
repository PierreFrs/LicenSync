using Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IUnitOfWork
{
    public interface IAlbumUploadUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();

        Task<IDbContextTransaction> BeginTransactionAsync();
        
        Task CommitTransactionAsync();
        
        Task RollbackTransactionAsync();
    }
}
