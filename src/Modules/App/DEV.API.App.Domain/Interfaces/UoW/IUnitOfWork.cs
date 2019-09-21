using DEV.API.App.Domain.Interfaces.Repositories.Entities;

namespace DEV.API.App.Domain.Interfaces.UoW
{
    public interface IUnitOfWork
    {
        IPersonRepository Person { get; }
    }
}
