
using BloodMuAPI.DataModel.Data;
using BloodMuAPI.DataProvider;
using BloodMuAPI.DataProvider.API;
using BloodMuAPI.Services.API;
using Microsoft.EntityFrameworkCore;

namespace BloodMuAPI.Services
{
    public class AccountService : IAccountService
    {
        private BloodMuDbContext _db { get; set; }
        private ILogger<IAccountService> _logger { get; set; }
        public AccountService(IBloodMuDbContext db, ILogger<IAccountService> logger)
        {
            _db = (BloodMuDbContext)db;
            _logger = logger;
        }

        public Account GetUsers()
        {
            var x = _db.Accounts
             .Include(c => c.Characters)
                 .ThenInclude(c => c.Inventory)
                     .ThenInclude(c => c.Items)
                         .ThenInclude(c => c.Definition)
             .Include(c => c.Characters)
                 .ThenInclude(c => c.Attributes)
                     .ThenInclude(c => c.Definition)
             .Include(c => c.Characters)
                 .ThenInclude(c => c.CharacterClass)
             .First();

            return x;
        }

        public Account? GetUser(string username, string password)
        {
            var account = _db.Accounts
            .Where(x => x.LoginName == username)
            .Include(c => c.Characters)
            .SingleOrDefault();

            if(account is not null && BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
            {
                return account;
            }

            return null;
        }
    }
}
