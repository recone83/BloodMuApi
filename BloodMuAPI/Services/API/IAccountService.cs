using BloodMuAPI.DataModel.Data;

namespace BloodMuAPI.Services.API
{
    public interface IAccountService
    {
        public Account GetUsers();
        public Account? GetUser(string username, string password);
        public Account? GetUser(string username);
        public bool AddAccount(AccountPost payload);
    }
}
