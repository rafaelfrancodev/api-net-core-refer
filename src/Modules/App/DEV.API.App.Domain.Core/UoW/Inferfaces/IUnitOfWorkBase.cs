using DEV.API.App.Domain.Core.Command;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Threading.Tasks;

namespace DEV.API.App.Domain.Core.UoW
{
    public interface IUnitOfWorkBase
    {
        bool CurrentTransaction();
        Task SaveChangesAsync();
        CommandResponse Commit();
        IDbContextTransaction DbTransaction { get; }
        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
    }
}
