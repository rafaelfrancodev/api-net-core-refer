using DEV.API.App.Domain.Interfaces.Repositories.Entities.Base;
using DEV.API.App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.API.App.Domain.Interfaces.Repositories.Entities
{
    public interface IPersonRepository : IRepository<Person, int>
    {
    }
}
