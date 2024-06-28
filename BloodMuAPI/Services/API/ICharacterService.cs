using BloodMuAPI.DataModel.Data;
using BloodMuAPI.DataModel.Data.Characters;

namespace BloodMuAPI.Services.API;

public interface ICharacterService
{
    public Task<AdvancedCharacter?>? GeCharacter(string name);
    public Task<List<SimpleCharacter>>? GeResets(int list = 10);
    public Task<List<AdvancedCharacter>>? GeAll(int list = 10);
    public Task<SystemStats> GetStats();
}
