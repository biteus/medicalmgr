using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models
{
    public class HealthStatistics
    {
            public IEnumerable<BloodSugar> BloodSugars { get; set; }
            public IEnumerable<BloodPressure> BloodPressure { get; set; }
            public IEnumerable<Medication> Medication { get; set; }
             public IEnumerable<Appointment> Appointments { get; set; }

            public String Title { get; set; }   
            public List<int> Sugars { get; set; }
            public List<string> ReadingTime { get; set; }
            // Average
            public String ThirtyDaysAvgSystolic { get; set; } 
            public String ThirtyDaysAvgDiastolic { get; set; } 
            public String ThirtyDaysAvgSugar { get; set; } 
            public String ThirtyDaysAvgPulse { get; set; } 
            // High
            public String ThirtyDaysHighSystolic { get; set; }
            public String ThirtyDaysHighDiastolic { get; set; } 
            public String ThirtyDaysHighSugar { get; set; } 
            public String ThirtyDaysHighPulse { get; set; } 

            // Low
            public String ThirtyDaysLowSystolic { get; set; }
            public String ThirtyDaysLowDiastolic { get; set; } 
            public String ThirtyDaysLowSugar { get; set; } 
            public String ThirtyDaysLowPulse { get; set; } 


    }
}
