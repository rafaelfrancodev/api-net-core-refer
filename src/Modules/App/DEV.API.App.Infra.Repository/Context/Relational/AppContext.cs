using DEV.API.App.Domain.Models;
using DEV.API.App.Infra.Repository.Configuration.Map.SqlServer;
using DEV.API.App.Infra.Repository.Configuration.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace DEV.API.App.Infra.Repository.Context.Relational
{
    [ExcludeFromCodeCoverage]
    public class AppContext : DbContext
    {
        private string _databaseDefault = "SqlServer";

        public AppContext(
             DbContextOptions<AppContext> options,
            IConfiguration configuration) : base(options)
        {
            _databaseDefault = configuration.GetSection("DefaultDatabase").Value;
        }

        #region DbSet

        public DbSet<Person> Person { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (_databaseDefault == "MySql")
                ConfigModelCreatingMySql(modelBuilder);
            else
                ConfigModelCreatingSqlServer(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigModelCreatingMySql(ModelBuilder modelBuilder)
        {
            modelBuilder
                .AddDefaultProperties()
                .ApplyConfiguration(new PersonMapMySql());
        }

        private void ConfigModelCreatingSqlServer(ModelBuilder modelBuilder)
        {
            modelBuilder
                .AddDefaultProperties()
                .ApplyConfiguration(new PersonMapSqlServer());
        }

        public override int SaveChanges()
        {
            ConfigPropertieDefault.SaveDefaultPropertiesChanges(ChangeTracker);
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            ConfigPropertieDefault.SaveDefaultPropertiesChanges(ChangeTracker);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
