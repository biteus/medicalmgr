using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MedicalManager.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the MedicalManagerUser class
    [Table("AspNetUsers")]
    public class MedicalManagerUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(20)")]
        public string Country { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [Required, MaxLength(20), MinLength(2)]
        public string Firstname { get; set; }
        
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        [Required, MaxLength(20), MinLength(2)]
        public string Lastname { get; set; }
    }
}
