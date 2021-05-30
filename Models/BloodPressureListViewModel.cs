using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models
{
    public class BloodPressureListViewModel
    {
            public IEnumerable<BloodPressure> BloodPressures { get; set; }
            public String Title { get; set; }
            public List<int> Systolic { get; set; }
            public List<int> Diastolic { get; set; }
            public List<string> ReadingTime { get; set; }
    }
}
