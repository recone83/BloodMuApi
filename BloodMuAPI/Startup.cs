using Microsoft.AspNetCore.Builder;
using Npgsql;
using System.Net;
using BloodMuAPI;

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
            services.AddScoped<NpgsqlConnection>(_ =>
            {
                var connection = new NpgsqlConnection(Configuration.GetConnectionString("MyConnection"));
                connection.Open();
                return connection;
            });

            /*
            services.AddHsts(options =>
             {
                 options.Preload = true;
                 options.IncludeSubDomains = true;
                 options.MaxAge = TimeSpan.FromDays(60);
                 options.ExcludedHosts.Add("example.com");
                 options.ExcludedHosts.Add("www.example.com");
             });
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 5001;
            });
            */

            services.AddControllers();
        //    services.AddEndpointsApiExplorer();
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
