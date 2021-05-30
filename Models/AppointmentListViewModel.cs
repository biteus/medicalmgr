using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models
{
    public class AppointmentListViewModel
    {
            public IEnumerable<Appointment> Appointments { get; set; }
            public String Title { get; set; }
            public List<int> DaysToAppointment { get; set; }
    }
}
