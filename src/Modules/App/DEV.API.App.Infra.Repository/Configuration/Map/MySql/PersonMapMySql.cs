using DEV.API.App.Domain.Models;
using DEV.API.App.Infra.Repository.Configuration.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace DEV.API.App.Infra.Repository.Configuration.Map.SqlServer
{
    [ExcludeFromCodeCoverage]
    public class PersonMapMySql : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("person").AddIsDeletedFilter();
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.DateBirthday).HasColumnName("date_birthday");
            builder.Property(x => x.Active).HasColumnName("active");
            builder.Property(x => x.IdUser).HasColumnName("id_user");           
            builder.Property(x => x.IdUserChange).HasColumnName("id_user_change");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
            builder.Property(x => x.Deleted).HasColumnName("deleted");
            builder.Property(p => p.Id).UseSqlServerIdentityColumn();
        }
    }
}
