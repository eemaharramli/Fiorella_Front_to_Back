using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Fiorella
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            // var connectionStringForWindows = this._configuration.GetConnectionString("ConnectionForWindows");
            //
            // services.AddDbContext<AppDbContext>(options =>
            // {
            //     options.UseSqlServer(connectionStringForWindows);
            // });

            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(1));
            
            var connectionString = this._configuration.GetConnectionString("DefaultConnection");
            
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddMvc().AddNewtonsoftJson(x=>x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseSession();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Bad Request 404");
            });
        }
    }
}