using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models.Interfaces
{
    public interface IAppointment
    {
        Appointment GetAppointment(int Id, string UserId);
        IEnumerable<Appointment> GetAllAppointment(string UserId);
        Appointment AddAppointment(Appointment appointment, string UserId);
        Appointment UpdateAppointment(Appointment appointment, string UserId);
        Appointment DeleteAppointment(int id, string UserId);
        Appointment EditAppointment(Appointment appointment, string UserId);
    }
}
