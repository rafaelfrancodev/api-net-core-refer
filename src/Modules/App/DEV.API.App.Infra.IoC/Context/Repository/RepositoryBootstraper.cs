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
            }
            else if (configuration.GetSection("DefaultDatabase").Value == "MySQL")
            {

            }
            else
            {
            }
        }
    }
}
