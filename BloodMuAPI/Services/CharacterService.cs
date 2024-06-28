using BloodMuAPI.DataProvider.API;
using BloodMuAPI.DataProvider;
using BloodMuAPI.Services.API;
using BloodMuAPI.DataModel.Data;
using Microsoft.EntityFrameworkCore;
using BloodMuAPI.DataModel.Data.Characters;

namespace BloodMuAPI.Services
{

    /// <summary>
    /// CharacterService
    /// </summary>
    public class CharacterService : ICharacterService
    {
        private BloodMuDbContext _db { get; set; }
        private ILogger<ICharacterService> _logger { get; set; }
        public CharacterService(IDbContextFactory<BloodMuDbContext> factory, ILogger<ICharacterService> logger)
        {
            _db = factory.CreateDbContext();
            _logger = logger;
        }

        /// <summary>
        /// Get character by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<AdvancedCharacter?> GeCharacter(string name)
        {
            var ch = _db.Characters?
                .Include(c => c.CurrentMap)
                .Include(c => c.CharacterClass)
                .Include(c => c.Attributes.Where(
                    s => s.Definition.Designation == "Level" || s.Definition.Designation == "Resets"
                    )
                )
                .ThenInclude(c => c.Definition)
                .Where<Character>(x => x.Name == name)
                .Select(row => new AdvancedCharacter
                {
                    Name = row.Name,
                    Class = row.CharacterClass.Name,
                    CurrentMap = row.CurrentMap.Name,
                    X = row.PositionX,
                    Y = row.PositionY,
                    Exp = row.Experience,
                    LVL = (int)(row.Attributes.First(x => x.Definition.Designation == "Level").Value),
                    Reset = (int)(row.Attributes.First(x => x.Definition.Designation == "Resets").Value)
                })
                .SingleOrDefaultAsync();

            return ch;
        }

        /// <summary>
        /// Collect server stats
        /// </summary>
        /// <returns></returns>
        public async Task<SystemStats> GetStats()
        {
            var stats = new SystemStats
            {
                Characters = await _db.Characters.CountAsync(),
                Accounts = await _db.Accounts.CountAsync()
            };

            return stats;
        }

        /// <summary>
        /// Get resets list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Task<List<SimpleCharacter>?>? GeResets(int list)
        {
             var ch = _db.Characters?
            .Include(c => c.CharacterClass)
            .Include(c => c.Attributes)
                .ThenInclude(c => c.Definition)
            .Select(x => new SimpleCharacter
                {
                    Name = x.Name,
                    Class = x.CharacterClass.Name,
                    Resets = x.Attributes.Where(s => s.Definition.Designation == "Resets").First().Value
                })
            .Where(x => x.Resets > 0)
            .OrderByDescending(x => x.Resets)
            .Take(list)
            .ToListAsync();

            return ch;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Task<List<AdvancedCharacter>>? GeAll(int list = 10)
        {
            var ch = _db.Characters?
            .Include(c => c.CurrentMap)
            .Include(c => c.CharacterClass)
            .Include(c => c.Attributes.Where(
                s => s.Definition.Designation == "Level" || s.Definition.Designation == "Resets"
                )
            )
            .ThenInclude(c => c.Definition)
            .Select(row => new AdvancedCharacter
            {
                Name = row.Name,
                Class = row.CharacterClass.Name,
                CurrentMap = row.CurrentMap.Name,
                X = row.PositionX,
                Y = row.PositionY,
                Exp = row.Experience,
                LVL = (int)(row.Attributes.First(x => x.Definition.Designation == "Level").Value),
                Reset = (int)(row.Attributes.First(x => x.Definition.Designation == "Resets").Value)
            })
            .OrderBy(x => x.Name)
            .Take(list)
            .ToListAsync();

            return ch;
        }
    }
}
