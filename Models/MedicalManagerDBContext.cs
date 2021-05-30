using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalManager.Areas.Identity.Data;
using MedicalManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedicalManager.Data
{
    public class MedicalManagerDBContext : IdentityDbContext<User>
    {
        public MedicalManagerDBContext(DbContextOptions<MedicalManagerDBContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        //public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
        public virtual DbSet<BloodPressure> BloodPressures { get; set; }
        public virtual DbSet<BloodSugar> BloodSugars { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Medication>().HasOne<User>(a => a.AppUser).WithMany(b => b.Medications).HasForeignKey(b => b.UerID);
        }
    }
}
