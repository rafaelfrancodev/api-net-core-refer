using DEV.API.App.Domain.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace DEV.API.App.Infra.Repository.Configuration.Properties
{
    [ExcludeFromCodeCoverage]
    public static class ConfigPropertieDefaul
    {
        internal static bool IsEntity(Type type)
        {
            var isEntity = type.BaseType == typeof(EntityAudited<>);
            if (isEntity)
                return true;
            if (type.BaseType != null)
                return IsEntity(type.BaseType);
            else
                return false;
        }
        internal static ModelBuilder AddDefaultProperties(this ModelBuilder modelBuilder)
        {
            var listEntities = modelBuilder.Model.GetEntityTypes().Where(x => !x.IsQueryType);

            foreach (var entityType in listEntities)
            {
                if (typeof(IEntityAudited).IsAssignableFrom(entityType.ClrType))
                {
                    if (entityType.FindProperty("Id") != null)
                    {
                        modelBuilder.Entity(entityType.Name).Property<DateTime?>("CreatedAt");
                        modelBuilder.Entity(entityType.Name).Property<DateTime?>("UpdatedAt");
                        modelBuilder.Entity(entityType.Name).Property<int>("IdUserChange");
                        modelBuilder.Entity(entityType.Name).Property<bool>("Deleted");
                    }
                }
            }

            return modelBuilder;
        }

        internal static EntityTypeBuilder AddIsDeletedFilter<T>(this EntityTypeBuilder<T> entityTypeBuilder) where T : EntityModel
        {
            entityTypeBuilder.HasQueryFilter(c => !EF.Property<bool>(c, "Deleted"));
            return entityTypeBuilder;
        }

        internal static void SaveDefaultPropertiesChanges(ChangeTracker changeTracker)
        {
            foreach (var entry in changeTracker.Entries()
               .Where(e => (e.State == EntityState.Added) && e.Entity is IEntityAudited))
            {
                entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                //if (userAuthOn != null)
                //    entry.Property("IdUserChange").CurrentValue = userAuthOn.Id;
            }


            foreach (var entry in changeTracker.Entries()
                .Where(e => (e.State == EntityState.Modified) && e.Entity is IEntityAudited))
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                //if (userAuthOn != null)
                //    entry.Property("IdUserChange").CurrentValue = userAuthOn.Id;
            }

            foreach (var entry in changeTracker.Entries()
                .Where(p => p.State == EntityState.Deleted
                            && p.Entity is IEntityAudited))
            {
                entry.Property("Deleted").CurrentValue = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
