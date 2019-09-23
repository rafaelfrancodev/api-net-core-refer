using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEV.API.App.Domain.Interfaces.Entities.Base
{
    public interface IDomainServiceBase<TEntity, TKey>
     where TEntity : class
     where TKey : struct
    {
        Task<TEntity> InsertAsync(TEntity obj);
        Task<TEntity> UpdateAsync(TKey id, TEntity obj);
        Task DeleteAsync(TEntity obj);
        Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity> GetByIdAsync(TKey id);
    }
}
