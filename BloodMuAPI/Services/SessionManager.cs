using BloodMuAPI.DataModel.Data;
using BloodMuAPI.Services.API;

public class SessionManager : ISessionManager
{
    private AccountSession _userSession { get; set; }

    public bool SetSessionUser(AccountSession _user)
    {
        _userSession = _user;
        return true;
    }

    public AccountSession GetSessionUser()
    {
        return _userSession;
    }
}