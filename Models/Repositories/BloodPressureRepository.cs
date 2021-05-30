using MedicalManager.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalManager.Areas.Identity.Data;
using MedicalManager.Data;
using MedicalManager.Models;
using Microsoft.Extensions.Logging;

namespace MedicalManager.Models.Repositories
{
    public class BloodPressureRepository : IBloodPressure
    {
        private readonly MedicalManagerDBContext _dbContext;
        private readonly ILogger<BloodPressureRepository> _logger;

        public BloodPressureRepository(MedicalManagerDBContext dbContext, ILogger<BloodPressureRepository> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        public BloodPressure AddBloodPressure(BloodPressure bp, string UserId)
        {
            bp.UerID = UserId;
            _dbContext.BloodPressures.Add(bp);
            _dbContext.SaveChanges();
            return bp;
        }

        public BloodPressure DeleteBloodPressure(int id, string UserId)
        {
            throw new NotImplementedException();
        }

        public BloodPressure EditBloodPressuren(BloodPressure bp, string UserId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BloodPressure> GetAllBloodPressure(string UserId)
        {
            return _dbContext.BloodPressures.Where(m => m.UerID.Contains(UserId));
        }

        public BloodPressure GetBloodPressure(int Id, string UserId)
        {
            throw new NotImplementedException();
        }

        public BloodPressure UpdateBloodPressure(BloodPressure bp, string UserId)
        {
            throw new NotImplementedException();
        }

        //public MedicationRepository(MedicalManagerDBContext dbContext,  ILogger<MedicationRepository> logger)
        //{
        //    this._dbContext = dbContext;
        //    this._logger = logger;
        //}


        //public Medication AddMedication(Medication medication, string UserId)
        //{
        //    medication.UerID = UserId;
        //    _dbContext.Medications.Add(medication);
        //    _dbContext.SaveChanges();
        //    return medication;
        //}

        //public Models.Medication DeleteMedication(int id, string UserId)
        //{
        //    Medication medication = _dbContext.Medications.Where(item => item.Id == id && item.UerID.Contains(UserId)).FirstOrDefault();
        //    if (medication != null)
        //    {
        //        _dbContext.Medications.Remove(medication);
        //        _dbContext.SaveChanges();
        //    }
        //    return medication;
        //}

        //public Models.Medication EditMedication(Models.Medication medication, string UserId)
        //{
        //    medication.UerID = UserId;
        //    _dbContext.Medications.Update(medication);
        //    _dbContext.SaveChanges();
        //    return medication;
        //}

        //public IEnumerable<Models.Medication> GetAllMedication(string UserId)
        //{
        //    return _dbContext.Medications.Where(m => m.UerID.Contains(UserId));
        //}

        //public Models.Medication GetMedication(int Id, string UserId)
        //{
        //    Medication medication = _dbContext.Medications.Where(item => item.Id == Id && item.UerID.Contains(UserId)).FirstOrDefault();
        //    return medication;

        //}

        //public Medication UpdateMedication(Medication medication, string UserId)
        //{
        //    //Medication medication = _dbContext.Medications.Where(item => item.Id == id && item.UerID.Contains(UserId)).FirstOrDefault();
        //    medication.UerID = UserId;
        //    if (medication != null)
        //    {

        //        var model = new Medication
        //        {
        //            Id = medication.Id,   //What the Id value should be. Should not  it be the  same to impact the same entry in DB? 
        //            Name = medication.Name,
        //            Dose = medication.Dose,
        //            Administer = medication.Administer,
        //            Prescribed = medication.Prescribed,
        //            Refills = medication.Refills,
        //            Prescriber = medication.Prescriber,
        //            UerID = medication.UerID,
        //            AppUser = medication.AppUser,
        //            Notes = medication.Notes
        //        };
        //        var updatedMedication = _dbContext.Medications.Attach(model);
        //        updatedMedication.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        _dbContext.SaveChanges();

        //        //updatedMedication.Context.SaveChanges();
        //        //updatedMedication.Entry(myedication).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        //updatedMedication.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //        //_dbContext.SaveChanges();
        //    }

        //    return medication;
        //}
    }
}
