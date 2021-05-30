using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalManager.Models;

namespace MedicalManager.Models
{
    public enum TakeTime
    {
        Morning,
        Lunch,
        Evening,
        Night
    }

    public enum Count
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }

     public enum AppointmentType
    {
        Doctor,
        Lab,
        Prcedure,
        Hospital,
        UrgentCare,
        FollowUp
    }


    public enum ReminderType
    {
        Email,
        Phone
    }

    public enum ReminderDays
    {
        One,
        Two,
        Three,

        Four,
        Five,

        Six,
        Seven,

        Eight,

        Nine,

        Ten

    }

    //public static class MedicationTime
    //{
    //    public static string MORNING { get; } = "Morning";
    //    static readonly string LUNCH = "Lunch";
    //    static readonly string EVENING = "Evening";
    //    static readonly string MORNING_NIGHT = "Morning, Night";
    //    static readonly string MORNING_LUNCH = "Morning, Lunch";
    //    static readonly string MORNING_EVENING = "Morning, Evening";
    //    static readonly string LUNCH_NIGHT = "Lunch, Night";
    //    static readonly string LUNCH_EVENING = "Lunch, Evening";
    //    static readonly string MORNING_LUNCH_NIGHT = "Morning, Lunch, Night";
    //    static readonly string MORNING_LUNCH_EVENING = "Morning, Lunch, Evening";
    //    static readonly string EVERY_4_HOURS = "Every 4 Hours";
    //    static readonly string EVERY_6_HOURS = "Every 6 Hours";
    //    static readonly string EVERY_12_HOURS = "Every 6 Hours";
    //}

    //public enum TakeTime
    //{
      
    //}

}
