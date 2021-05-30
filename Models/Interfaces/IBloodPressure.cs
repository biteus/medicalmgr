using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models.Interfaces
{
    public interface IBloodPressure
    {
        BloodPressure GetBloodPressure(int Id, string UserId);
        IEnumerable<BloodPressure> GetAllBloodPressure(string UserId);
        BloodPressure AddBloodPressure(BloodPressure bp, string UserId);
        BloodPressure UpdateBloodPressure(BloodPressure bp, string UserId);
        BloodPressure DeleteBloodPressure(int id, string UserId);
        BloodPressure EditBloodPressuren(BloodPressure bp, string UserId);
    }
}
