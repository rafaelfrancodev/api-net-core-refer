using DEV.API.App.Domain.Core.Model;
using DEV.API.App.Domain.Core.Notification;
using DEV.API.App.Domain.Interfaces.Entities;
using DEV.API.App.Domain.Service;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace DEV.API.App.Infra.IoC.Context.Domain
{
    [ExcludeFromCodeCoverage]
    internal class DomainBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IPersonDomainService, PersonDomainService>();
        }
    }
}
