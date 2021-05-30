using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MedicalManager.Models;
using MedicalManager.Data;

namespace MedicalManager.Models
{
    public static class DBMigrationHandler
    {
       public static void DBPreSet(IApplicationBuilder app)
       {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                System.Console.WriteLine("Applying Migration...");
                SeedData(serviceScope.ServiceProvider.GetService<MedicalManagerDBContext>());
            }
        }

        public static void SeedData(MedicalManagerDBContext context)
        {
            System.Console.WriteLine("Applying Migrations..");
            context.Database.Migrate();
        }

    }
}
