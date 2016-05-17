
namespace Repository.Pattern.Ef6.Infrastructure
{
    public abstract class ConcurrencyCheckEntity<TKey> : BaseEntity<TKey> where TKey : struct
    {
        /// <summary>
        /// Gets the row version.
        /// </summary>
        public byte[] RowVersion { get; internal set; }
    }
}
