using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainCarAPI.Model.Entity
{
    public abstract class AbstractEntity
    {
        /// <summary>
        /// With the annotations above, the Id field will be the primary key of the database tables.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// The Deleted field is required for the soft-delete.
        /// True -> soft-deleted, but still in database
        /// </summary>
        public bool Deleted { get; set; }
    }
}
