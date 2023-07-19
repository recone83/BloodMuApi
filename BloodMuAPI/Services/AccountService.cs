
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

        public async Task<Account?> GetUser(string username, string password)
        {
            var account = await _db.Accounts
                .Where(x => x.LoginName == username)
                .Include(c => c.Characters)
                .SingleOrDefaultAsync();

            if(account is not null && BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
            {
                return account;
            }

            return null;
        }

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
                SecurityCode = "1234",
                VaultPassword = "",
                IsVaultExtended = false
            };

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.Accounts?.Add(account);
                    await _db.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                } catch (Exception ex) {
                    transaction.Rollback();
                    _db.ChangeTracker.Clear();
                    _logger.LogError(ex.Message);
                }
            }

            return false;
        }

        public void test()
        {
            /*
            _db.Accounts
            .FromSql(" SELECT ch.Name, "
            + " st.Value as Level,  st1.Value as Reset "
            + " FROM [Character] ch "
            + " JOIN [StatAttribute] st WITH ch.Id = st.CharacterId AND st.DefinitionId = (SELECT ad1.Id FROM [AttributeDefinition] ad1 WHERE ad1.Id = st.DefinitionId AND ad1.Designation LIKE 'Level')"
            + " JOIN [StatAttribute] st1 WITH ch.Id = st1.CharacterId AND st1.DefinitionId = (SELECT ad2.Id FROM [AttributeDefinition] ad2 WHERE ad2.Id = st1.DefinitionId AND ad2.Designation LIKE 'Resets')"
            + " ORDER BY st.Value+st1.Value*1000 DESC")
            .ToList();
            */
        }
    }
}
