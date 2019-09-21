using DEV.API.App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

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


        }

        private void ConfigModelCreatingSqlServer(ModelBuilder modelBuilder)
        {

        }
    }
}
