namespace BloodMuAPI.DataProvider
{
    using BloodMuAPI.DataModel.Config;
    using BloodMuAPI.DataModel.Data;
    using BloodMuAPI.DataProvider.API;
    using Microsoft.EntityFrameworkCore;

    public class BloodMuDbContext : DbContext, IBloodMuDbContext
    {
        public BloodMuDbContext(DbContextOptions<BloodMuDbContext> options) : base(options)
        {
        }

        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Character>? Characters { get; set; }

        // config
        public DbSet<StatAttribute>? Attributes { get; set; }
        public DbSet<AttributeDefinition>? Definition { get; set; }
        public DbSet<GameMapDefinition>? Maps { get; set; }
        public DbSet<CharacterClass>? Class { get; set; }
    }
}