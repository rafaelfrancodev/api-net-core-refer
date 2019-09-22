namespace DEV.API.App.Domain.Core.UoW
{
    [ExcludeFromCodeCoverage]
    public abstract class UnitOfWorkBase<TIUnitOfWork> : UnitOfWorkBase
         where TIUnitOfWork : IUnitOfWorkBase
    {

        public UnitOfWorkBase(TIUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notification) : base(unitOfWork, notification)
        {
        }
    }

    public abstract class UnitOfWorkBase
    {
        private readonly IUnitOfWorkBase _unitOfWork;
        private readonly DomainNotificationHandler _messageHandler;

        public UnitOfWorkBase(IUnitOfWorkBase unitOfWork,
            INotificationHandler<DomainNotification> notification)
        {
            _unitOfWork = unitOfWork;
            _messageHandler = (DomainNotificationHandler)notification;
        }

        protected bool Commit()
        {
            if (_messageHandler == null)
                return false;

            if (_messageHandler.HasNotifications())
                return false;

            var commandReponse = _unitOfWork.Commit();
            if (commandReponse.Success)
                return true;

            return false;
        }
    }
