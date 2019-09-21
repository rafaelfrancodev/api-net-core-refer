

using DEV.API.App.Domain.Interfaces.Repositories.Entities;
using DEV.API.App.Domain.Models;
using DEV.API.App.Infra.Repository.Context.Relational;
using DEV.API.App.Infra.Repository.Repositories.Entities.Base;
using System.Diagnostics.CodeAnalysis;

namespace DEV.API.App.Infra.Repository.Repositories.Entities
{
    [ExcludeFromCodeCoverage]
    public class PersonRepository : RepositoryBase<Person, int, AppContext>, IPersonRepository
    {
        public PersonRepository(AppContext context) : base (context)
        {
        }
    }
}
