using BloodMuAPI.DataModel.Data.Accounts;

namespace BloodMuAPI.Services.API
{
    public interface IAccountService
    {
        public Task<Account> GetUsers();
        public Task<Account?> GetUser(Login data);
        public Task<Account?> GetUser(string username);
        public Task<bool> AddAccount(AccountPost payload);
    }
}
