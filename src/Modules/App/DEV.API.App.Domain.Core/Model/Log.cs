using MongoDB.Bson;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DEV.API.App.Domain.Core.Model
{
    [ExcludeFromCodeCoverage]
    public class Log<T>
    {
        public Log()
        {
        }

        public Log(T idRegister, string type, string schema, string table, int? idUser, int? idPerson, string namePerson, string operation,  string registerBefore = "", string registerNow = "")
        {
            IdRegister = idRegister;
            CreatedAt = DateTime.Now;
            Type = type;
            Schema = schema;
            Table = table;
            IdUser = idUser;
            IdPerson = idPerson;
            NamePerson = namePerson;
            Operation = operation;

            if (!string.IsNullOrWhiteSpace(registerBefore))
                RegisterBefore = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(registerBefore);

            if (!string.IsNullOrWhiteSpace(registerNow))
                RegisterNow = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(registerNow);
        }

        public ObjectId _id { get; set; }
        public T IdRegister { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }
        public string Schema { get; set; }
        public string Table { get; set; }
        public int? IdUser { get; set; }       
        public int? IdPerson { get; set; }
        public string NamePerson { get; set; }
        public string Operation { get; set; }
        public BsonDocument RegisterBefore { get; set; }
        public BsonDocument RegisterNow { get; set; }
    }
}
