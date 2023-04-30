
using Microsoft.EntityFrameworkCore;
using BloodMuAPI.DataProvider;
using BloodMuAPI.Services.API;
using BloodMuAPI.Services;
using BloodMuAPI.DataProvider.API;
using BloodMuAPI.Extensions;

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
            services.AddSwaggerGen(c=>
            {
                //c.EnableAnnotations();
                //c.OperationFilter<CustomHeaderSwaggerAttribute>();
            });
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
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
