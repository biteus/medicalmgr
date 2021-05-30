using MedicalManager.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MedicalManager.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Only alphabet of size 2 to 50 allowed")]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }
        // [Required]
        public string Location { get; set; }
        [Required]
        [Display(Name = "Place To Take")]
        public string Suite { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
       

        //public List<string> Administer { get; set; }
        [Required]
        [Display(Name = "Schedule Date")]
        public DateTime Scheduled { get; set; }
        //[Required]
        public string Days { get; set; }

        public AppointmentType AppointmentType {get; set;}

        public ReminderDays FirstReminder  {get; set;}
        
        public ReminderDays SecondReminder  {get; set;}

        public ReminderType ReminderType  {get; set;}
        
        [Display(Name = "Notes")]
        [MaxLength(100)]
        public string Notes { get; set; }

        public string UerID { get; set; }
        public virtual User AppUser {get; set;}

    }
}
