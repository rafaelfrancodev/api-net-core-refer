using DEV.API.App.Domain.Core.Model;
using DEV.API.App.Domain.Core.UoW.Inferfaces;
using DEV.API.App.Domain.Interfaces.Entities;
using DEV.API.App.Domain.Interfaces.Repositories.Entities;
using DEV.API.App.Domain.Interfaces.Repositories.Entities.Base;
using DEV.API.App.Domain.Interfaces.UoW;
using DEV.API.App.Domain.Models;
using DEV.API.App.Domain.Service.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEV.API.App.Domain.Service
{
    public class PersonDomainService : DomainServiceBase<Person, int, IUnitOfWork>, IPersonDomainService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ISmartNotification _notification;

        public PersonDomainService(
            IPersonRepository personRepository,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> messageHandler,
            IRepositoryLog<int> repositoryLog,
            ISmartNotification notification
            ) : base(unitOfWork, personRepository, repositoryLog, messageHandler)
        {
            _personRepository = personRepository;
            _notification = notification;
        }
    }
}
