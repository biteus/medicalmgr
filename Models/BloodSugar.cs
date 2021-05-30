using MedicalManager.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace MedicalManager.Models
{
    public class BloodSugar
    {
        public int Id { get; set; }

        
        [Required]
        [Display(Name = "Blood Sugar")]
        public int Sugar { get; set; }

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
