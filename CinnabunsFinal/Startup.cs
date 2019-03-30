using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VueCliMiddleware;

namespace CinnabunsFinal
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors();

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<AppContext>(options =>
                {
                    options.UseNpgsql(Configuration["DBCONNECTION"]);
                });

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<AppContext>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(configuration =>
            {
                configuration.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials();
            });

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSpa(configuration =>
            {
                configuration.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                    configuration.UseVueCli("serve", 3000);
            });
            app.UseMvc();
        }
    }
}
