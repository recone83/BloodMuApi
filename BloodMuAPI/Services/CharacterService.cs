﻿using BloodMuAPI.DataProvider.API;
using BloodMuAPI.DataProvider;
using BloodMuAPI.Services.API;
using BloodMuAPI.DataModel.Data;
using Microsoft.EntityFrameworkCore;
namespace BloodMuAPI.Services
{
    public class SimpleCharacter
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public float Resets { get; set; }
    }

    public class CharacterService : ICharacterService
    {
        private BloodMuDbContext _db { get; set; }
        private ILogger<ICharacterService> _logger { get; set; }
        public CharacterService(IBloodMuDbContext db, ILogger<ICharacterService> logger)
        {
            _db = (BloodMuDbContext)db;
            _logger = logger;
        }

        public Character? GeCharacter(string name)
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
                .SingleOrDefault();

            return ch;
        }
        public List<SimpleCharacter> GeResets(int list = 10)
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
            .ToList();

            return ch;
        }
    }
}
