using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models.Interfaces
{
    public interface IMedication
    {
        Medication GetMedication(int Id, string UserId);
        IEnumerable<Medication> GetAllMedication(string UserId);
        Medication AddMedication(Medication medication, string UserId);
        Medication UpdateMedication(Medication medication, string UserId);
        Medication DeleteMedication(int id, string UserId);
        Medication EditMedication(Medication medication, string UserId);
    }
}
