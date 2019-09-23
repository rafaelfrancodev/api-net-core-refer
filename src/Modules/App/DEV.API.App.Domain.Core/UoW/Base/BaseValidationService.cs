using DEV.API.App.Domain.Core.Model;
using DEV.API.App.Domain.Core.UoW.Inferfaces;

namespace DEV.API.App.Domain.Core.UoW.Base
{
    public class BaseValidationService
    {
        private readonly ISmartNotification _notify;

        public BaseValidationService(ISmartNotification notify)
        {
            _notify = notify.Invoke();
        }

        protected void NotificarErrosValidacao(EntityModel message)
        {
            foreach (var error in message.ValidationResult.Errors)
                _notify.NewNotificationBadRequest(error.PropertyName, error.ErrorMessage);
        }
    }
}
