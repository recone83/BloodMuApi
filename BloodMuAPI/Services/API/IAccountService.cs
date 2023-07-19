using BloodMuAPI.DataModel.Data;

namespace BloodMuAPI.Services.API
{
    public interface IAccountService
    {
        public Task<Account> GetUsers();
        public Task<Account?> GetUser(string username, string password);
        public Task<Account?> GetUser(string username);
        public Task<bool> AddAccount(AccountPost payload);
    }
}
