namespace Repository.Pattern.Ef6.Infrastructure
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using Repository.Pattern.Infrastructure;

    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class BaseEntity<TKey> : BaseEntity where TKey : struct
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public TKey Id { get; set; }

        private static bool IsTransient(BaseEntity<TKey> obj)
        {
            return obj != null && Equals(obj.Id, default(TKey));
        }

        public virtual bool Equals(BaseEntity<TKey> other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(this.Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = this.GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            if (Equals(this.Id, default(TKey)))
                return base.GetHashCode();
            return this.Id.GetHashCode();
        }

        public Type GetUnproxiedType()
        {
            return this.GetType();
        }
        public static bool operator ==(BaseEntity<TKey> x, BaseEntity<TKey> y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity<TKey> x, BaseEntity<TKey> y)
        {
            return !(x == y);
        }
        protected virtual void SetParent(dynamic child)
        {

        }
        protected virtual void SetParentToNull(dynamic child)
        {

        }
    }

    public abstract class BaseEntity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
