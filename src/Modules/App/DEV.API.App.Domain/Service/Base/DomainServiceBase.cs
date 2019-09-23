using DEV.API.App.Domain.Core.Model;
using DEV.API.App.Domain.Core.UoW;
using DEV.API.App.Domain.Interfaces.Entities.Base;
using DEV.API.App.Domain.Interfaces.Repositories.Entities.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEV.API.App.Domain.Service.Base
{
    public class DomainServiceBase<TEntity, TKey, TIUnitOfWork> : UnitOfWorkBase<TIUnitOfWork>, IDomainServiceBase<TEntity, TKey>
         where TEntity : EntityModel<TKey>
         where TKey : struct
        where TIUnitOfWork : IUnitOfWorkBase
    {
        protected readonly IRepository<TEntity, TKey> _repository;
        protected readonly TIUnitOfWork _unitOfWork;
        private readonly IRepositoryLog<TKey> _repositoryLog;

        public DomainServiceBase(
            TIUnitOfWork unitOfWork,
            IRepository<TEntity, TKey> repository,
            IRepositoryLog<TKey> repositoryLog,
            INotificationHandler<DomainNotification> messageHandler
            ) : base(unitOfWork, messageHandler)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;           
            _repositoryLog = repositoryLog;
        }


        public virtual async Task<TEntity> UpdateAsync(TKey id, TEntity obj)
        {
            using (_unitOfWork.BeginTransaction())
            {
                var objectBefore = await _repository.GetByIdAsync(id);
                await RegisterLog<TEntity, TKey>(obj, objectBefore, "UpdateAsync");
                var result = await _repository.UpdateAsync(id, obj);
                Commit();
                return result;
            }
        }
        public virtual async Task DeleteAsync(TEntity entity)
        {
            using (_unitOfWork.BeginTransaction())
            {
                await RegisterLog<TEntity, TKey>(null, entity, "DeleteAsync");
                await _repository.DeleteAsync(entity);
                Commit();
            }
        }
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            using (_unitOfWork.BeginTransaction())
            {
                var result = await _repository.InsertAsync(entity);
                Commit();
                await RegisterLog<TEntity, TKey>(entity, null, "InsertAsync");
                return result;
            }
        }
        public virtual async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _repository.SelectAsync(where);
        }
        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _repository.GetByIdAsync(id);
        }

        [ExcludeFromCodeCoverage]
        public virtual async Task RegisterLog<TEntity, TKey>(TEntity entityNow, TEntity entityBefore, string operation)
            where TEntity : EntityModel<TKey>
           where TKey : struct
        {
            if (_repository.Context == null) return;

            var entityDefault = entityNow == null ? entityBefore : entityNow;
            var type = entityDefault.GetType();
            var schemaAnnotation = _repository.Context.Model.FindEntityType(type).GetAnnotations();

            var registerNow = entityNow != null ? JsonConvert.SerializeObject(entityNow, Formatting.Indented, Settings<TEntity, TKey>()) : "";
            var registerBefore = entityBefore != null ? JsonConvert.SerializeObject(entityBefore, Formatting.Indented, Settings<TEntity, TKey>()) : "";

            var schema = schemaAnnotation.FirstOrDefault(x => x.Name == "Relational:Schema");
            var table = schemaAnnotation.FirstOrDefault(x => x.Name == "Relational:TableName");

            var schemaName = "";
            var tableName = "";

            if (schema != null)
                schemaName = string.IsNullOrWhiteSpace(schema.Value.ToString()) ? "" : schema.Value.ToString();

            if (table != null)
                tableName = string.IsNullOrWhiteSpace(table.Value.ToString()) ? "" : table.Value.ToString();

            var log = new Log<TKey>(
                entityDefault.Id,
                type.ToString(),
                schemaName,
                tableName,
                null,
                null,
                "",
                operation,
                registerBefore,
                registerNow
            );

            await _repositoryLog.InsertLogAsync(log);
        }

        [ExcludeFromCodeCoverage]
        private static JsonSerializerSettings Settings<TEntity, TKey>() where TEntity : EntityModel<TKey> where TKey : struct
        {
            return new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }

        [ExcludeFromCodeCoverage]
        public async Task RegisterLogUpdate<TEntity, TKey>(TKey id, TEntity entityNow)
            where TEntity : EntityModel<TKey>
            where TKey : struct
        {
            if (_repository.Context == null) return;

            var entityBefore = _repository.Context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
            await RegisterLog<TEntity, TKey>(entityNow, entityBefore, "UpdateAsync");
        }

        [ExcludeFromCodeCoverage]
        public async Task RegisterLogInsert<TEntity, TKey>(TEntity entityNow)
            where TEntity : EntityModel<TKey>
            where TKey : struct
        {
            if (_repository.Context == null) return;

            await RegisterLog<TEntity, TKey>(entityNow, null, "InsertAsync");
        }

        [ExcludeFromCodeCoverage]
        public async Task RegisterLogDelete<TEntity, TKey>(TKey id)
            where TEntity : EntityModel<TKey>
            where TKey : struct
        {
            if (_repository.Context == null) return;

            var entityBefore = _repository.Context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
            await RegisterLog<TEntity, TKey>(null, entityBefore, "DeleteAsync");
        }
    }
}
