using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DEV.API.App.Domain.Interfaces.Repositories.Entities.Base
{
    public interface IRepository : IDisposable
    {
        DbContext Context { get; }
    }

    public interface IRepository<TEntity, TKey> : IRepository
       where TEntity : class
       where TKey : struct
    {
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TKey id, TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> where);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where);
    }
}
