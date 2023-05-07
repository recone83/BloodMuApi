using BloodMuAPI.DataModel.Data;

namespace BloodMuAPI.Services.API
{
    public interface ICharacterService
    {
        public Character? GeCharacter(string name);
        public List<SimpleCharacter> GeResets(int list = 10);
    }
}
