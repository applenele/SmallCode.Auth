using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmallCode.Auth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using SmallCode.Auth.Services;

namespace SmallCode.Auth
{
    public class Startup
    {



        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AuthContext>(option => option.UseNpgsql(Configuration.GetConnectionString("PostgreSql")));

            services.AddMvc();
            services.AddSession();
            services.AddAuthorization();
            services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

            //csrf
            services.AddAntiforgery();

            services.AddScoped<ICacheService, CacheService>();

        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();

            app.UseSession();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = new PathString("/Account/Login"),
                AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,

            });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
            }
            );

            await SampleData.InitDB(app.ApplicationServices);
        }

    }
}
