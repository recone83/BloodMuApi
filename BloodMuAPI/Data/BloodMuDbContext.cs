namespace BloodMuAPI.Data
{
    using BloodMuAPI.DataModel.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class BloodMuDbContext : DbContext
    {
        public BloodMuDbContext(DbContextOptions<BloodMuDbContext> options) : base(options)
        {
        }

        public DbSet<Account>? Accounts { get; set; }

    }
}
