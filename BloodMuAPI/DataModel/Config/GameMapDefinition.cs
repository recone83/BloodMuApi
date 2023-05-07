using System.ComponentModel.DataAnnotations.Schema;

namespace BloodMuAPI.DataModel.Config
{
    [Table("GameMapDefinition", Schema = "config")]
    public class GameMapDefinition : Entity
    {
        /// <summary>
        /// Gets or sets the number of the map.
        /// </summary>
        /// <remarks>
        /// This number is identifying the map on the client.
        /// </remarks>
        public short Number { get; set; }

        /// <summary>
        /// Gets or sets the name of the map.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the exp multiplier for this map.
        /// </summary>
        /// <value>
        /// The exp multiplier.
        /// </value>
        public double ExpMultiplier { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Number} - {this.Name}";
        }
    }
}
