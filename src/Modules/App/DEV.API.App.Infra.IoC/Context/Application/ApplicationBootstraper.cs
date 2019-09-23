using DEV.Api.App.Application.Interfaces;
using DEV.Api.App.Application.Services.AppPerson;
using DEV.API.App.Domain.Core.Inferfaces;
using DEV.API.App.Domain.Core.Interfaces;
using DEV.API.App.Domain.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace DEV.API.App.Infra.IoC.Context.Application
{
    [ExcludeFromCodeCoverage]
    internal class ApplicationBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services)
        {
            services.AddScoped<ISmartNotification, SmarNotificationCoreService>();
            services.AddScoped<IStringLocalization, StringLocalizationCoreService>();
            services.AddScoped<IPersonAppService, PersonAppService>();
        }
    }
}
