using DEV.API.App.Domain.Interfaces.Repositories.Entities;
using DEV.API.App.Domain.Interfaces.UoW;
using DEV.API.App.Infra.Repository.Context.Relational;
using DEV.API.App.Infra.Repository.Repositories.Entities;
using DEV.API.App.Infra.Repository.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DEV.API.App.Infra.IoC.Context.Repository
{
    internal class RepositoryBootstraper
    {
        internal void ChildServiceRegister(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetSection("DefaultDatabase").Value == "SqlServer")
            {
                services.AddDbContext<AppContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
            }
            else if (configuration.GetSection("DefaultDatabase").Value == "MySql")
            {
                services.AddDbContext<AppContext>(options => options.UseMySql(configuration.GetConnectionString("MySql")));
            }
            else
            {
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
}
