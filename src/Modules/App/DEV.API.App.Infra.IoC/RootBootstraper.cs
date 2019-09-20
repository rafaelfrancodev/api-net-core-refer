using System;
using System.Diagnostics.CodeAnalysis;
using DEV.API.App.Infra.IoC.Context.Application;
using DEV.API.App.Infra.IoC.Context.Domain;
using DEV.API.App.Infra.IoC.Context.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DEV.API.App.Infra.IoC
{
    [ExcludeFromCodeCoverage]
    public class RootBootstrapper
    {
        public void RootRegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            new ApplicationBootstraper().ChildServiceRegister(services);
            new DomainBootstraper().ChildServiceRegister(services);
            new RepositoryBootstraper().ChildServiceRegister(services, configuration);
            new RepositoryNoSQLBootstraper().ChildServiceRegister(services, configuration);
        }
    }
}
