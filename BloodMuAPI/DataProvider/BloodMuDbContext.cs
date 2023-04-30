namespace BloodMuAPI.DataProvider
{
    using BloodMuAPI.DataModel.Data;
    using BloodMuAPI.DataProvider.API;
    using Microsoft.EntityFrameworkCore;

    public class BloodMuDbContext : DbContext, IBloodMuDbContext
    {
        public BloodMuDbContext(DbContextOptions<BloodMuDbContext> options) : base(options)
        {
        }

        public DbSet<Account>? Accounts { get; set; }

    }
}
