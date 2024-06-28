using BloodMuAPI.DataModel.Data.Accounts;

namespace BloodMuAPI.Services.API
{
    public interface ISessionManager
    {
        public bool SetSessionUser(AccountSession _user);
        public AccountSession GetSessionUser();
    }
}
