// <copyright file="AttributeDefinition.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace BloodMuAPI.DataModel.Config;

/// <summary>
/// Defines and Identifies a Attribute.
/// In the future it may also contain additional data, like a maximum limit of the reachable value to do balancing.
/// </summary>
[Table("AttributeDefinition", Schema = "config")]
public class AttributeDefinition : Entity
{
    public string? Designation { get; set; }

    public string? Description { get; set; }
}