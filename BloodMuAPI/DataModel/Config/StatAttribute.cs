using System.ComponentModel.DataAnnotations.Schema;

namespace BloodMuAPI.DataModel.Config;

[Table("StatAttribute", Schema = "data")]
public class StatAttribute : Entity
{
    public float Value { get; set; }
    public AttributeDefinition Definition { get; set; }
}
