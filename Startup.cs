using MedicalManager.Areas.Identity.Data;
using MedicalManager.Data;
using MedicalManager.Models;
using MedicalManager.Models.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Data.SqlClient;
//using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

namespace MedicalManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc().AddXmlSerializerFormatters();
            
            var connectionStr = Configuration.GetConnectionString("AwsRDS");
            Console.WriteLine("Connection String RDS: " + connectionStr);
            SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder(connectionStr);
            builder.UserID="medicalmgradmin";
            builder.Password="";
            var connection =  builder.ConnectionString;
            services.AddDbContext<MedicalManagerDBContext>(
                options => options.UseSqlServer(connection)
                );
            services.AddIdentity<User, IdentityRole>(config =>
            {
                //config.SignIn.RequireConfirmedEmail = true;
                config.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<MedicalManagerDBContext>().AddDefaultUI().AddDefaultTokenProviders();

            services.AddScoped<IMedication, Models.Repositories.MedicationRepository>();
            services.AddScoped<IBloodPressure, Models.Repositories.BloodPressureRepository>();
            services.AddScoped<IBloodSugar, Models.Repositories.BloodSugarRepository>();
            services.AddScoped<IAppointment, Models.Repositories.AppointmentRepository>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home";
                    await next();
                }
            });

            DBMigrationHandler.DBPreSet(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            
        }
    }
}
