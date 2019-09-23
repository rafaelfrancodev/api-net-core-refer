namespace DEV.API.App.Domain.Core.Inferfaces
{
    public interface ISmartNotification
    {
        ISmartNotification Invoke();
        bool IsValid();
        void NewNotificationConflict(string key, params string[] parameters);
        void NewNotificationBadRequest(string key, params string[] parameters);
    }
}
