
namespace Repository.Pattern.Ef6.Infrastructure
{
    using System;

    using Repository.Pattern.Ef6.Enums;

    public abstract class EditableEntity<TKey> : BaseEntity<TKey> where TKey : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditableEntity{TKey}"/> class.
        /// </summary>
        public EditableEntity()
        {
            this.CreateDate = DateTime.Now;
            this.Status = GeneralStatus.Active;
        }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public GeneralStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the create date
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the update date
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }
}
