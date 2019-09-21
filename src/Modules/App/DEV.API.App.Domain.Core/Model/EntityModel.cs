using FluentValidation.Results;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEV.API.App.Domain.Core.Model
{
    public abstract class EntityModel
    {
        [BsonIgnore]
        [JsonIgnore]
        [NotMapped]
        public ValidationResult ValidationResult { get; set; }
        public virtual bool IsValid()
        {
            return true;
        }
    }
    public abstract class EntityModel<T> : EntityModel
    {
        [BsonIgnore]
        public T Id { get; protected set; }
        public virtual void SetId(T id)
        {
            this.Id = id;
        }

    }

    public interface IEntityAudited
    {
    }

    public abstract class EntityAudited<T> : EntityModel<T>, IEntityAudited where T : struct
    {
        public bool Deleted { get; protected set; }
        public int IdUserChange { get; protected set; }
        public DateTime? CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        public static bool operator !=(EntityAudited<T> a, EntityAudited<T> b)
        {
            return !(a == b);
        }

        public static bool operator ==(EntityAudited<T> a, EntityAudited<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityAudited<T>;

            if (ReferenceEquals(this, compareTo))
                return true;
            if (ReferenceEquals(null, compareTo))
                return false;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() * 907 + Id.GetHashCode();
        }
    }
}
