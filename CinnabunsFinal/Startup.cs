using CinnabunsFinal.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VueCliMiddleware;

namespace CinnabunsFinal
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = environment;
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

            services.Configure<IdentityOptions>(options =>
            {
                if (Environment.IsDevelopment())
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 1;
                    options.Password.RequiredUniqueChars = 1;
                }
            });

            var authScheme = "JwtBearer";
            var signingKey = Configuration["SECRETKEY"];
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = authScheme;
                options.DefaultChallengeScheme = authScheme;
            })
            .AddJwtBearer(authScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
                };
            });

            services.AddAuthorization();
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

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc();
            app.UseSpa(configuration =>
            {
                configuration.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                    configuration.UseVueCli("serve", 3000);
            });
        }
    }
}
