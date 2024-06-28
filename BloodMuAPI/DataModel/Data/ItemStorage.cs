// <copyright file="ItemStorage.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

using System.ComponentModel.DataAnnotations.Schema;

namespace BloodMuAPI.DataModel.Data;

/// <summary>
/// A storage where items can be stored.
/// </summary>
[Table("ItemStorage", Schema = "data")]
public class ItemStorage : Entity
{
    /// <summary>
    /// Gets or sets the items which are stored.
    /// </summary>
    ///[MemberOfAggregate]
    public virtual ICollection<Item> Items { get; protected set; } = null!;

    /// <summary>
    /// Gets or sets the money which is stored.
    /// </summary>
    public int Money { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Items?.Count ?? 0} Items, {Money} Money";
    }
}