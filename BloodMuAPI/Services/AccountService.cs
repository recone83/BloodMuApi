
using BloodMuAPI.DataModel.Data;
using BloodMuAPI.DataModel.Data.Accounts;
using BloodMuAPI.DataProvider;
using BloodMuAPI.DataProvider.API;
using BloodMuAPI.Services.API;
using Microsoft.EntityFrameworkCore;

namespace BloodMuAPI.Services
{
    /// <summary>
    /// AccountService
    /// </summary>
    public class AccountService : IAccountService
    {
        private BloodMuDbContext _db { get; set; }
        private ILogger<IAccountService> _logger { get; set; }
        public AccountService(IDbContextFactory<BloodMuDbContext> factory, ILogger<IAccountService> logger)
        {
            _db = factory.CreateDbContext();
            _logger = logger;
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <returns></returns>
        public async Task<Account> GetUsers()
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
                 .ThenInclude(c => c.CharacterClass);

            return await x.FirstAsync(); ;
        }

        /// <summary>
        /// Get specyfic user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Account?> GetUser(Login data)
        {
            var account = await _db.Accounts
                .Where(x => x.LoginName == data.Username)
                .Include(c => c.Characters)
                .SingleOrDefaultAsync();

            if(account is not null && BCrypt.Net.BCrypt.Verify(data.Password, account.PasswordHash))
            {
                return account;
            }

            return null;
        }

        /// <summary>
        /// Get user by name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Account?> GetUser(string username)
        {
            var account = await _db.Accounts
                .Where(x => x.LoginName == username)
                .Include(c => c.Characters)
                    .ThenInclude(c => c.Inventory)
                .Include(c => c.Characters)
                    .ThenInclude(c => c.CharacterClass)
                .SingleOrDefaultAsync();

            return account;
        }

        /// <summary>
        /// Add account
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public async Task<bool> AddAccount(AccountPost payload)
        {
            var account = new Account()
            {
                EMail = payload.EMail,
                LoginName = payload.LoginName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(payload.Password),
                Vault = new ItemStorage()
                {
                    Money = 5000
                },
                State = 0,
                VaultPassword = "",
                IsVaultExtended = false
            };

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.Accounts?.Add(account);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                } catch (Exception ex) {
                    transaction.Rollback();
                    _db.ChangeTracker.Clear();
                    _logger.LogError(ex.Message);
                }
            }

            return false;
        }
    }
}
