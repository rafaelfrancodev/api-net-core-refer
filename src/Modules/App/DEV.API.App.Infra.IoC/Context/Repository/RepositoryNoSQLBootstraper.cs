using DEV.API.App.Domain.Interfaces.Repositories.Entities.Base;
using DEV.API.App.Infra.Repository.Repositories.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DEV.API.App.Infra.IoC.Context.Repository
{
    internal class RepositoryNoSQLBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepositoryLog<int>, RepositoryLog<int>>(provider => new RepositoryLog<int>(configuration));
        }
    }
}
