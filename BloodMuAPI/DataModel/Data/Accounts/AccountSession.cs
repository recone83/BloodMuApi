// <copyright file="Account.cs" company="MUnique">
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace BloodMuAPI.DataModel.Data.Accounts;

public class AccountSession
{
    public string LoginName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the e mail address.
    /// </summary>
    public string EMail { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the registration date.
    /// </summary>
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    public AccountState State { get; set; }

    /// <summary>
    /// Gets or sets the timezone of the player, difference to UTC.
    /// </summary>
    public short TimeZone { get; set; }

    /// <summary>
    /// Gets or sets the characters.
    /// </summary>
    public virtual List<string> Characters { get; set; }

}