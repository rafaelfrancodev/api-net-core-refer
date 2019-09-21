
using DEV.API.App.Domain.Core.Model;
using DEV.API.App.Domain.Interfaces.Repositories.Entities.Base;
using DEV.API.App.Infra.Repository.Context.NoSql;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace DEV.API.App.Infra.Repository.Repositories.Entities
{
    [ExcludeFromCodeCoverage]
    public class RepositoryLog<T> : IRepositoryLog<T>
    {
        private readonly AppNoSqlContext<T> _context;

        public RepositoryLog(IConfiguration _configuracao)
        {
            var servidor = _configuracao.GetSection("ConnectionMongo").Value;
            var database = _configuracao.GetSection("DatabaseMongoLog").Value;
            _context = new AppNoSqlContext<T>(servidor, database);
        }
        
        public async Task InsertLogAsync(Log<T> log)
        {
            await _context.Log.InsertOneAsync(log);
        }
    }
}
