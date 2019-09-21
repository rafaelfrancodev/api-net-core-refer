using DEV.API.App.Domain.Core.Model;
using DEV.API.App.Domain.Interfaces.Repositories.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEV.API.App.Infra.Repository.Repositories.Entities.Base
{
    [ExcludeFromCodeCoverage]
    public abstract class RepositoryBase<TEntity, TContext> : IRepository, IDisposable
       where TEntity : class
       where TContext : DbContext
    {
        protected int _timeout => 30000;
        protected readonly DbSet<TEntity> _db;
        protected readonly TContext _context;
        public DbContext Context => _context;

        protected RepositoryBase(TContext context)
        {
            _context = context;
            _db = _context.Set<TEntity>();
            _context.Database.SetCommandTimeout(_timeout);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    [ExcludeFromCodeCoverage]
    public abstract class RepositoryBase<TEntity, TKey, TContext> : RepositoryBase<TEntity, TContext>, IRepository<TEntity, TKey>
        where TEntity : EntityModel<TKey>
        where TContext : DbContext
        where TKey : struct
    {
        protected RepositoryBase(TContext context) : base(context)
        {
        }

        public virtual async Task<TEntity> UpdateAsync(TKey id, TEntity entity)
        {
            var currentEntity = await GetByIdAsync(id).ConfigureAwait(false);
            _context.Entry(currentEntity).CurrentValues.SetValues(entity);
            return entity;
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            return Task.FromResult(_db.Remove(entity));
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _db.AddAsync(entity);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> where)
        {
            var query =
                 await _db
                    .Where(where)
                    .OrderByDescending(ent => ent.Id)
                    .AsNoTracking()
                    .ToListAsync()
                    .ConfigureAwait(false);

            return query;
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            Expression<Func<TEntity, bool>> predicate = t => t.Id.Equals(id);
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _db.AnyAsync(where);
        }
    }
}
