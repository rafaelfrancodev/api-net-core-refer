using DEV.API.App.Domain.Core.Enum;
using MediatR;
using Newtonsoft.Json;

namespace DEV.API.App.Domain.Core.Model
{
    public class DomainNotification : INotification
    {
        public string Key { get; }
        public string Value { get; }

        [JsonIgnore]
        public DomainNotificationType DomainNotificationType { get; }

        public DomainNotification(
            string key,
            string value,
            DomainNotificationType type = DomainNotificationType.Conflict)
        {
            Key = key;
            Value = value;
            DomainNotificationType = type;
        }
    }
}
