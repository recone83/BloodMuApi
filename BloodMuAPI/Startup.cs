
using Microsoft.EntityFrameworkCore;
using BloodMuAPI.DataProvider;
using BloodMuAPI.Services.API;
using BloodMuAPI.Services;
using BloodMuAPI.DataProvider.API;

namespace BloodMuAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IBloodMuDbContext, BloodMuDbContext>(config =>
            {
                config.UseNpgsql(Configuration.GetConnectionString("BloodMuDbConnaction"));
            }, ServiceLifetime.Singleton);

            services.AddSingleton<IAccountService, AccountService>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseAuthentication(); // Enables authentication for the request processing pipeline
            app.UseAuthorization(); // Enables authorization for the request processing pipeline
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
