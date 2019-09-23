using DEV.API.App.Domain.Core.UoW;
using DEV.API.App.Domain.Interfaces.Repositories.Entities;

namespace DEV.API.App.Domain.Interfaces.UoW
{
    public interface IUnitOfWork : IUnitOfWorkBase
    {
        IPersonRepository Person { get; }
    }
}
