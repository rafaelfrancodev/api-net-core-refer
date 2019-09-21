using DEV.API.App.Domain.Models;
using DEV.API.App.Infra.Repository.Configuration.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace DEV.API.App.Infra.Repository.Configuration.Map.SqlServer
{
    [ExcludeFromCodeCoverage]
    public class PersonMapSqlServer : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person", "common").AddIsDeletedFilter();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.Name).HasColumnName("Name");
            builder.Property(x => x.DateBirthday).HasColumnName("DateBirthday");
            builder.Property(x => x.Active).HasColumnName("Active");
            builder.Property(x => x.IdUser).HasColumnName("IdUser");           
            builder.Property(x => x.IdUserChange).HasColumnName("IdUserChange");
            builder.Property(x => x.CreatedAt).HasColumnName("CreatedAt");
            builder.Property(x => x.UpdatedAt).HasColumnName("UpdatedAt");
            builder.Property(x => x.Deleted).HasColumnName("Deleted");
            builder.Property(p => p.Id).UseSqlServerIdentityColumn();
        }
    }
}
