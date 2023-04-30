using System.ComponentModel.DataAnnotations;

namespace BloodMuAPI.DataModel
{
    public abstract class Entity
    {
        [Key]
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }
    }
}
