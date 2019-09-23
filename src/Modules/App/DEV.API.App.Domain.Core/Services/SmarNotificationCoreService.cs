

using DEV.API.App.Domain.Core.Enum;
using DEV.API.App.Domain.Core.Inferfaces;
using DEV.API.App.Domain.Core.Interfaces;
using DEV.API.App.Domain.Core.Model;
using DEV.API.App.Domain.Core.Notification;
using MediatR;
using System.Threading;

namespace DEV.API.App.Domain.Core.Services
{
    public class SmarNotificationCoreService : ISmartNotification
    {
        private readonly DomainNotificationHandler _messageHandler;
        private readonly IStringLocalization _localizer;

        public SmarNotificationCoreService(INotificationHandler<DomainNotification> notification,
            IStringLocalization localizer)
        {
            _messageHandler = (DomainNotificationHandler)notification;
            _localizer = localizer;
            _localizer.SetLocalizationValueList();
        }

        public ISmartNotification Invoke()
        {
            return this;
        }

        public bool IsValid()
        {
            return !_messageHandler.HasNotifications();
        }

        public void NewNotificationConflict(string key, params string[] parameters)
        {
            var completeMessage = _localizer[key]?.Value;
            if (completeMessage == null)
                return;

            completeMessage = GettingParametrizedMessage(parameters, completeMessage);

            _messageHandler.Handle(new DomainNotification(key, completeMessage), default(CancellationToken));
        }

        public void NewNotificationBadRequest(string key, params string[] parameters)
        {
            var completeMessage = _localizer[key]?.Value;
            if (completeMessage == null)
                return;

            completeMessage = GettingParametrizedMessage(parameters, completeMessage);

            _messageHandler.Handle(new DomainNotification(key, completeMessage, DomainNotificationType.BadRequest), default(CancellationToken));
        }

        private static string GettingParametrizedMessage(string[] parameters, string completeMessage)
        {
            var externalCounter = 0;
            foreach (var parameter in parameters)
            {
                completeMessage = completeMessage.Replace("{0}", parameter);
                externalCounter++;

                completeMessage = completeMessage.Replace("{" + externalCounter + "}", "{0}");
            }

            return completeMessage;
        }
    }
}
