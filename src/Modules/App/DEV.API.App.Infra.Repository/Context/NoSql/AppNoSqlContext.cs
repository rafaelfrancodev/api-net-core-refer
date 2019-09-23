using DEV.API.App.Domain.Core.Model;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace DEV.API.App.Infra.Repository.Context.NoSql
{
    [ExcludeFromCodeCoverage]
    public class AppNoSqlContext<T>
    {
        private readonly IMongoDatabase _mongoDatabase;
        public AppNoSqlContext(string servidor, string database)
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            var client = new MongoClient(servidor);
            _mongoDatabase = client.GetDatabase(database);
        }

        public IMongoCollection<Log<T>> GetCollection<T>()
        {
            return _mongoDatabase.GetCollection<Log<T>>("Log");
        }
    }

}
