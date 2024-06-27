using BloodMuAPI.DataModel.Data;

namespace BloodMuAPI.Services.API
{
    public interface ICharacterService
    {
        public Task<Character?>? GeCharacter(string name);
        public Task<List<SimpleCharacter>>? GeResets(int list = 10);
        public Task<SystemStats> GetStats();
    }
}
