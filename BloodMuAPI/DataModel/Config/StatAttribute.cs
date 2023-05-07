using BloodMuAPI.DataModel.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodMuAPI.DataModel.Config;

[Table("StatAttribute", Schema = "data")]
public class StatAttribute : Entity
{
    public float Value { get; set; }
    public Guid CharacterId { get; set; }

    [ForeignKey("CharacterId")]
    public virtual Character? Character { get; set; }
    public AttributeDefinition Definition { get; set; }
}
