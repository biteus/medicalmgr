using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models
{
    public  static class MedicationTime
    {
        public static List<string> listMedicationTime = new List<string> {
            "Morning", "Lunch", "Evening", "Morning, Night", "Morning, Lunch", "Morning, Evening", "Lunch, Night", "Lunch, Evening", "Morning, Lunch, Night", "Morning, Lunch, Evening",
            "Every 4 Hours", "Every 6 Hours", "Every 6 Hours"
        };
        public static string MORNING { get; } = "Morning";
        static readonly string LUNCH = "Lunch";
        static readonly string EVENING = "Evening";
        static readonly string MORNING_NIGHT = "Morning, Night";
        static readonly string MORNING_LUNCH = "Morning, Lunch";
        static readonly string MORNING_EVENING = "Morning, Evening";
        static readonly string LUNCH_NIGHT = "Lunch, Night";
        static readonly string LUNCH_EVENING = "Lunch, Evening";
        static readonly string MORNING_LUNCH_NIGHT = "Morning, Lunch, Night";
        static readonly string MORNING_LUNCH_EVENING = "Morning, Lunch, Evening";
        static readonly string EVERY_4_HOURS = "Every 4 Hours";
        static readonly string EVERY_6_HOURS = "Every 6 Hours";
        static readonly string EVERY_12_HOURS = "Every 6 Hours";
    }
}
