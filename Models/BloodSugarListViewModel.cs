using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models
{
    public class BloodSugarListViewModel
    {
            public IEnumerable<BloodSugar> BloodSugars { get; set; }
            public String Title { get; set; }   
             
            public List<int> Sugars { get; set; }
            public List<string> ReadingTime { get; set; }
    }
}
