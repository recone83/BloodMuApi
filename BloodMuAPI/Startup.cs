
using Microsoft.EntityFrameworkCore;
using BloodMuAPI.DataProvider;
using BloodMuAPI.Services.API;
using BloodMuAPI.Services;
using BloodMuAPI.DataProvider.API;
using BloodMuAPI.Extensions;
using Microsoft.Extensions.Options;

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
                config.UseNpgsql(Configuration.GetConnectionString("BloodMuDbConnection"));
            }, ServiceLifetime.Singleton);

            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<ICharacterService, CharacterService>();
            services.AddSingleton<ISessionManager, SessionManager>();

            services.AddScoped<AuthSessionHandler>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
        {
            if (env.IsDevelopment()) {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                    app.UseCors(x => x.AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true) 
                        .AllowCredentials());
                } else {
                    if (!String.IsNullOrEmpty(config["AllowedOrigins"])) {
                        var origins = config["AllowedOrigins"].Split(";");
                        app.UseCors(x => x
                            .WithOrigins(origins)
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .AllowAnyHeader());
                    }
                }

            app.UseRouting();
            app.UseSession();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
