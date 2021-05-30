using MedicalManager.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalManager.Areas.Identity.Data;
using MedicalManager.Data;
using MedicalManager.Models;
using Microsoft.Extensions.Logging;

namespace MedicalManager.Models.Repositories
{
    public class AppointmentRepository : IAppointment
    {
        private readonly MedicalManagerDBContext _dbContext;
        private readonly ILogger<AppointmentRepository> _logger;

        public AppointmentRepository(MedicalManagerDBContext dbContext,  ILogger<AppointmentRepository> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }


        public Appointment AddAppointment(Appointment appointment, string UserId)
        {
            appointment.UerID = UserId;
            _dbContext.Appointments.Add(appointment);
            _dbContext.SaveChanges();
            return appointment;
        }

        public Appointment DeleteAppointment(int id, string UserId)
        {
            Appointment appointment = _dbContext.Appointments.Where(item => item.Id == id && item.UerID.Contains(UserId)).FirstOrDefault();
            if (appointment != null)
            {
                _dbContext.Appointments.Remove(appointment);
                _dbContext.SaveChanges();
            }
            return appointment;
        }

        public Appointment EditAppointment(Appointment appointment, string UserId)
        {
            appointment.UerID = UserId;
            _dbContext.Appointments.Update(appointment);
            _dbContext.SaveChanges();
            return appointment;
        }

        public IEnumerable<Appointment> GetAllAppointment(string UserId)
        {
            return _dbContext.Appointments.Where(m => m.UerID.Contains(UserId));
        }

        public Appointment GetAppointment(int Id, string UserId)
        {
            Appointment appointment = _dbContext.Appointments.Where(item => item.Id == Id && item.UerID.Contains(UserId)).FirstOrDefault();
            return appointment;

        }

        public Appointment UpdateAppointment(Appointment appointment, string UserId)
        {
            appointment.UerID = UserId;
            if (appointment != null)
            {
                var model = new Appointment
                {
                    Id = appointment.Id,
                    Name = appointment.Name,
                    Location = appointment.Location,
                    Suite = appointment.Suite,
                    Street = appointment.Street,
                    City = appointment.City,
                    Zip = appointment.Zip,
                    Country = appointment.Country,
                    Scheduled = appointment.Scheduled,
                    Days = appointment.Days,
                    AppointmentType = appointment.AppointmentType,
                    FirstReminder = appointment.FirstReminder,
                    ReminderType = appointment.ReminderType,
                    Notes = appointment.Notes,
                    UerID = appointment.UerID,
                    AppUser = appointment.AppUser,
                };
                var updatedAppointment = _dbContext.Appointments.Attach(model);
                updatedAppointment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();            
            }
            
            return appointment;
        }
    }
}
