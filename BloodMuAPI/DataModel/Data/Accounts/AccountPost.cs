using System.ComponentModel.DataAnnotations;

namespace BloodMuAPI.DataModel.Data.Accounts
{
    public class AccountPost
    {
        [Required]
        public string LoginName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EMail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
