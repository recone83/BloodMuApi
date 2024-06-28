namespace BloodMuAPI.DataModel.Data.Characters;

/// <summary>
/// AdvancedCharacter
/// </summary>
public class AdvancedCharacter
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Class
    /// </summary>
    public string? Class { get; set; }
    /// <summary>
    /// CurrentMap
    /// </summary>
    public string? CurrentMap { get; set; }
    /// <summary>
    /// X
    /// </summary>
    public int X { get; set; }
    /// <summary>
    /// Y
    /// </summary>
    public int Y { get; set; }
    /// <summary>
    /// Exp
    /// </summary>
    public long Exp { get; set; }
    /// <summary>
    /// LVL
    /// </summary>
    public int? LVL { get; set; }
    /// <summary>
    /// Reset
    /// </summary>
    public int? Reset { get; set; }
}
