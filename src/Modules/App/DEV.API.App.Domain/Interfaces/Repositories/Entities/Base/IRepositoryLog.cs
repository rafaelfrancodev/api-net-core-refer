using DEV.API.App.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DEV.API.App.Domain.Interfaces.Repositories.Entities.Base
{
    public interface IRepositoryLog<T>
    {
        Task InsertLogAsync(Log<T> log);
    }
}
