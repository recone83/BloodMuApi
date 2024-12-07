using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BloodMuAPI.DataModel.Data.Accounts
{
    public class Login
    {
        [Required]
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
