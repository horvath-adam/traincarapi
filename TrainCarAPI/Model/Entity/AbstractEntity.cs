using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainCarAPI.Model.Entity
{
    public abstract class AbstractEntity
    {
        /// <summary>
        /// Adatbázis azonostó
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// Logikai törlést jelző flag
        /// </summary>
        public bool Deleted { get; set; }
    }
}
