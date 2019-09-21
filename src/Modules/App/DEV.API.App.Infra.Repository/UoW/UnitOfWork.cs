using DEV.API.App.Domain.Core.Command;
using DEV.API.App.Domain.Interfaces.Repositories.Entities;
using DEV.API.App.Domain.Interfaces.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace DEV.API.App.Infra.Repository.UoW
{
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork
    {
        readonly Context.Relational.AppContext _context;
        public IServiceProvider _services { get; set; }

        public UnitOfWork(Context.Relational.AppContext context, IServiceProvider serviceProvider)
        {
            var serviceCollection = new ServiceCollection();
            _services = serviceProvider;
            _context = context;
        }

        public IDbContextTransaction DbTransaction { get; private set; }

        public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            return DbTransaction ?? (DbTransaction = _context.Database.BeginTransaction(isolationLevel));
        }

        public CommandResponse Commit()
        {
            if (DbTransaction == null)
                return new CommandResponse(false);

            _context.SaveChanges();
            _context.Database.CurrentTransaction.Commit();

            DbTransaction = null;
            return new CommandResponse(true);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool CurrentTransaction()
        {
            return _context.Database.CurrentTransaction == null ? false : true;
        }

        public IPersonRepository Person => _services.GetRequiredService<IPersonRepository>();
    }
}
