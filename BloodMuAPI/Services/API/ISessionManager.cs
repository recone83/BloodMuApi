using BloodMuAPI.DataModel.Data;

namespace BloodMuAPI.Services.API
{
    public interface ISessionManager
    {
        public bool SetSessionUser(AccountSession _user);
        public AccountSession GetSessionUser();
    }
}
