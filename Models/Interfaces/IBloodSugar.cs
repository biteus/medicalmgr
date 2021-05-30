using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Models.Interfaces
{
    public interface IBloodSugar
    {
        BloodSugar GetBloodSugar(int Id, string UserId);
        IEnumerable<BloodSugar> GetAllBloodSugar(string UserId);
        BloodSugar AddBloodSugar(BloodSugar bs, string UserId);
        BloodSugar UpdateBloodSugar(BloodSugar bs, string UserId);
        BloodSugar DeleteBloodSugar(int id, string UserId);
        BloodSugar EditBloodSugar(BloodSugar bs, string UserId);
    }
}
