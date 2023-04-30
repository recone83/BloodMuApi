// <copyright file="Item.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace BloodMuAPI.DataModel.Entities;

using BloodMuAPI.DataModel;
using BloodMuAPI.DataModel.Configuration.Items;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

/// <summary>
/// The item.
/// </summary>
[Table("Item", Schema = "data")]
public class Item : Entity
{
    /// <summary>
    /// Gets or sets the item slot in the <see cref="ItemStorage"/>.
    /// </summary>
    public byte ItemSlot { get; set; }

    [ForeignKey("DefinitionId")]
    public  ItemDefinition? Definition { get; set; }

    /// <summary>
    /// Gets or sets the currently remaining durability.
    /// </summary>
    public double Durability { get; set; }

    /// <summary>
    /// Gets or sets the level of the item.
    /// </summary>
    public byte Level { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this item instance provides the weapon skill while being equipped.
    /// </summary>
    public bool HasSkill { get; set; }

    /// <summary>
    /// Gets or sets the item options.
    /// </summary>
    //[MemberOfAggregate]
  //  public virtual ICollection<ItemOptionLink> ItemOptions { get; protected set; } = null!;

    /// <summary>
    /// Gets or sets the applied item set groups (Ancient Set).
    /// </summary>
  //  public virtual ICollection<ItemOfItemSet> ItemSetGroups { get; protected set; } = null!;

    /// <summary>
    /// Gets or sets the socket count. This limits the amount of socket options in the <see cref="ItemOptions"/>.
    /// </summary>
    public int SocketCount { get; set; }

    /// <summary>
    /// Gets or sets the price which was set by the player for his personal store.
    /// </summary>
    public int? StorePrice { get; set; }

    /// <summary>
    /// Gets or sets the pet experience.
    /// Only applies, if this item is actually a trainable pet.
    /// </summary>
    public int PetExperience { get; set; }

    /*
    public void AssignValues(Item otherItem)
    {
        Definition = otherItem.Definition;
        Durability = otherItem.Durability;
        Level = otherItem.Level;
        HasSkill = otherItem.HasSkill;
        SocketCount = otherItem.SocketCount;
        PetExperience = otherItem.PetExperience;
        if (otherItem.ItemOptions != null && otherItem.ItemOptions.Any())
        {
            ItemOptions.Clear();
            foreach (var option in otherItem.ItemOptions)
            {
                ItemOptions.Add(CloneItemOptionLink(option));
            }
        }

        if (otherItem.ItemSetGroups != null && otherItem.ItemSetGroups.Any())
        {
            ItemSetGroups.Clear();
            foreach (var setGroup in otherItem.ItemSetGroups)
            {
                ItemSetGroups.Add(setGroup);
            }
        }
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("Slot ").Append(ItemSlot).Append(": ");

        if (ItemOptions.Any(o => o.ItemOption?.OptionType == ItemOptionTypes.Excellent))
        {
            stringBuilder.Append("Excellent ");
        }

        var ancientSet = ItemSetGroups.FirstOrDefault(s => s.AncientSetDiscriminator != 0)?.ItemSetGroup;
        if (ancientSet != null)
        {
            stringBuilder.Append(ancientSet.Name).Append(" ");
        }

        stringBuilder.Append(Definition?.Name);
        if (Level > 0)
        {
            stringBuilder.Append("+").Append(Level);
        }

        foreach (var option in ItemOptions
                     .Where(o => o.ItemOption?.OptionType != ItemOptionTypes.Luck)
                     .OrderBy(o => o.ItemOption?.OptionType == ItemOptionTypes.Option))
        {
            var levelOption = option.ItemOption?.LevelDependentOptions.FirstOrDefault(o => o.Level == (option.ItemOption.LevelType == LevelType.ItemLevel ? Level : option.Level));

        }

        if (HasSkill)
        {
            stringBuilder.Append("+Skill");
        }

        if (ItemOptions.Any(opt => opt.ItemOption?.OptionType == ItemOptionTypes.Luck))
        {
            stringBuilder.Append("+Luck");
        }

        if (SocketCount > 0)
        {
            stringBuilder.Append("+").Append(SocketCount).Append("S");
        }

        return stringBuilder.ToString();
    }

    protected virtual ItemOptionLink CloneItemOptionLink(ItemOptionLink link)
    {
        return link.Clone();
    }
*/
}