using MedicalManager.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MedicalManager.Models
{
    public class BloodPressure
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Systolic(Upper)")]
        public int Systolic { get; set; }

        [Required]
        [Display(Name = "Diastolic(Lower)")]
        public int Diastolic { get; set; }

        [Required]
        [Display(Name = "Pulse")]
        public int Pulse { get; set; }

        [Required]
        [Display(Name = "Reading Date")]
        public DateTime ReadingDate { get; set; }

        [Display(Name = "Read By")]
        public string Administor { get; set; }

        [Display(Name = "Notes")]
        [MaxLength(100)]
        public string Notes { get; set; }

        public string UerID { get; set; }
        public virtual User AppUser {get; set;}

    }
}
