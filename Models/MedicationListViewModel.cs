using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models
{
    public class MedicationListViewModel
    {
            public IEnumerable<Medication> Medications { get; set; }
            public String Title { get; set; }
    }
}
