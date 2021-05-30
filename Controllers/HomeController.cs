using MedicalManager.Models;
using MedicalManager.Models.Interfaces;
using MedicalManager.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBloodSugar _handlerBloodSugar;
        private readonly IBloodPressure _handlerBloodPressure;
        private readonly IMedication _handlerMedication;
        private readonly IAppointment _handlerAppointment;

        private readonly UserManager<User> _userManager;


        //IdentityUser user = _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        //public HomeController(IMedication repositoryMedication, ILogger<HomeController> logger)
        //{
        //    this._logger = logger;
        //    this._handlerMedication = repositoryMedication;
        //}
        public HomeController(IMedication repositoryMedication, IBloodPressure repositoryBloodPressure, IBloodSugar repositorySugar, UserManager<User> userManager, ILogger<HomeController> logger,  IAppointment repositoryAppointment)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._handlerMedication = repositoryMedication;
            this._handlerBloodPressure = repositoryBloodPressure;
            this._handlerBloodSugar = repositorySugar;
            this._handlerAppointment = repositoryAppointment; //_handlerAppointment
            
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {
            var userID = _userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(userID)){
                var model = _handlerMedication.GetAllMedication(userID);
                return View(model);
            }
            return View();
        }

        [Route("Home")]
         public IActionResult Home()
        {
            
            DateTime now = DateTime.Now;
            
            var userID = _userManager.GetUserId(User);
            int rangeBP = 1, rangeBS = rangeBP;
            int avgSys = 1, avgDia = avgSys, avgPulse = avgSys, avgSugar = avgSys;
            int highSys = 1, highDia = highSys, highPulse = highSys, highSugar = highSys;
            int lowSys = 1, lowDia = lowSys, lowPulse = lowSys, lowSugar = lowSys;

            
            var modelBS = _handlerBloodSugar.GetAllBloodSugar(userID);
            var modelBP = _handlerBloodPressure.GetAllBloodPressure(userID);
            var modelAppt = _handlerAppointment.GetAllAppointment(userID);
            if(modelBS != null){
                rangeBS = modelBS.Count() >= 30? 30 :  modelBS.Count();
                var adjBS = modelBS.Count() >= 30? modelBS.ToList().GetRange(modelBS.Count() - 30, 30).ToArray():  modelBS;
                
                avgSugar = (int)adjBS.Average(o => o.Sugar);
                highSugar = (int)adjBS.Max(o => o.Sugar);
                lowSugar = (int)adjBS.Min(o => o.Sugar);
            }

            var adjAppt = modelAppt.Any()? modelAppt.Where(e=> e.Scheduled > now).ToList() : null;
            int cntAppts = adjAppt.Count();
            if(cntAppts > 0){
                for (int i = 0; i < cntAppts; i++)
                {
                    var e = adjAppt.ElementAt(i).Scheduled;
                    var days = e.Date == DateTime.Now.Date? 0 :
                    (int)(e - DateTime.Now).TotalDays + 1;
                    adjAppt
                    .ElementAt(i).Days = days.ToString();
                }
            }
            
            if(modelBP != null){
                rangeBP = modelBP.Count() >= 30? 30 :  modelBP.Count();
                var adjBP = modelBP.Count() >= 30? modelBP.ToList().GetRange(modelBP.Count() - 30, 30).ToArray():  modelBP;
                avgSys = (int)adjBP.Average(o => o.Systolic);
                avgDia = (int)adjBP.Average(o => o.Diastolic);
                avgPulse = (int)adjBP.Average(o => o.Pulse);

                highSys = (int)adjBP.Max(o => o.Systolic);
                highDia = (int)adjBP.Max(o => o.Diastolic);
                highPulse = (int)adjBP.Max(o => o.Pulse);

                lowSys = (int)adjBP.Min(o => o.Systolic);
                lowDia = (int)adjBP.Min(o => o.Diastolic);
                lowPulse = (int)adjBP.Min(o => o.Pulse);
            }

           
            if (!string.IsNullOrEmpty(userID)){
                var modelHealthStatistics = new HealthStatistics
                {
                    ThirtyDaysAvgSystolic = avgSys.ToString(),
                    ThirtyDaysAvgDiastolic = avgDia.ToString(),
                    ThirtyDaysAvgPulse =  avgPulse.ToString(),
                    ThirtyDaysAvgSugar =  avgSugar.ToString(),

                    ThirtyDaysHighSystolic = highSys.ToString(),
                    ThirtyDaysHighDiastolic = highDia.ToString(),
                    ThirtyDaysHighSugar = highSugar.ToString(),
                    ThirtyDaysHighPulse =  highPulse.ToString(), 

                    ThirtyDaysLowSystolic = lowSys.ToString(),
                    ThirtyDaysLowDiastolic = lowDia.ToString(),
                    ThirtyDaysLowSugar = lowSugar.ToString(),
                    ThirtyDaysLowPulse = lowPulse.ToString(), 

                    Medication = _handlerMedication.GetAllMedication(userID),
                    BloodSugars = modelBS,
                    BloodPressure = modelBP,
                    Appointments = adjAppt

                };
                return View(modelHealthStatistics);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create(Medication medication)
        {
            if (!ModelState.IsValid) return View();
            var userID = _userManager.GetUserId(User);
            var medicationAdded = _handlerMedication.AddMedication(medication, userID);
            return RedirectToAction("details", new { id = medicationAdded.Id });

        }

        [Route("Delete")]
        public IActionResult Delete(int? id)
        {
            var homeDetailsViewModel = new HomeDetailsViewModel
            {   
                Medication = _handlerMedication.DeleteMedication(id ?? 1, _userManager.GetUserId(User)),
                Title = "Medication Delete"
            };
            return View(homeDetailsViewModel);
        }


        [Route("List")]
        public IActionResult List()
        {
            var modelMedicationListView = new MedicationListViewModel
            {
                Medications = _handlerMedication.GetAllMedication(_userManager.GetUserId(User)),
                Title = "Medication List"
            };
            return View(modelMedicationListView);
        }

        //[Route("Edit")]
        //public IActionResult Edit(int? id)
        //{
        //    if (!ModelState.IsValid) return View();
        //    var medicationUpdated = _handlerMedication.UpdateMedication((int) id, _userManager.GetUserId(User));
        //    return RedirectToAction("Details", new { id = medicationUpdated.Id });
        //}

        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var homeDetailsViewModel = new HomeDetailsViewModel
            {
                Medication = _handlerMedication.GetMedication((int)id, _userManager.GetUserId(User)),
                Title = "Medication Edit"
            };
            return View(homeDetailsViewModel.Medication);
        }

        [Route("Edit")]
        [HttpPost]
        public IActionResult Edit(Medication medication)
        {
            if (!ModelState.IsValid) return View();
            var medicationUpdated = _handlerMedication.UpdateMedication(medication, _userManager.GetUserId(User));
            return RedirectToAction("Details", new { id = medicationUpdated.Id });

        }

        [Route("Details")]
        public IActionResult Details(int? id)
        {
            var homeDetailsViewModel = new HomeDetailsViewModel
            {
                Medication = _handlerMedication.GetMedication((int)id, _userManager.GetUserId(User)),
                Title = "Medication Details"
            };
            return View(homeDetailsViewModel);
        }

        [Route("Appointment")]
        [HttpGet]
        public IActionResult Appointment()
        {
            return View();
        }


        [Route("Appointment")]
        [HttpPost]
        public IActionResult Appointment(Appointment appointment)
        {
            if (!ModelState.IsValid) return View();
            var userID = _userManager.GetUserId(User);
            var appointmentAdded = _handlerAppointment.AddAppointment(appointment, userID);
            return RedirectToAction("AppointmentDetails", new { id = appointmentAdded.Id });
        }


        [Route("AppointmentList")]
        public IActionResult AppointmentList()
        {
            
            var modelAppointListView = new AppointmentListViewModel
            {
                Appointments = _handlerAppointment.GetAllAppointment(_userManager.GetUserId(User)),
                Title = "Appointments"
            };
            
            //modelAppointListView.Appointments = modelAppointListView.Appointments.Where(e => e.Days = ((int)(DateTime.Now-e.Scheduled).TotalDays).ToString());
            for (int i = 0; i < modelAppointListView.Appointments.Count(); i++)
            {
                var e = modelAppointListView.Appointments.ElementAt(i).Scheduled;
                var days = e.Date == DateTime.Now.Date? 0 :
                (int)(e - DateTime.Now).TotalDays + 1;
                modelAppointListView.Appointments.ElementAt(i).Days = days.ToString();
            }
            return View(modelAppointListView);
        }


        [Route("AppointmentDetails")]
        public IActionResult AppointmentDetails(int? id)
        {
            var appointmentDetailsViewModel = new AppointmentDetailsViewModel
            {
                Appointment = _handlerAppointment.GetAppointment((int)id, _userManager.GetUserId(User)),
                Title = "Appointment Details"
            };
            return View(appointmentDetailsViewModel);
        }


        [Route("AppointmentDelete")]
        public IActionResult AppointmentDelete(int? id)
        {
            var appointmentDetailsViewModel = new AppointmentDetailsViewModel
            {   
                Appointment = _handlerAppointment.DeleteAppointment(id ?? 1, _userManager.GetUserId(User)),
                Title = "Appointment Delete"
            };
            return View(appointmentDetailsViewModel);
        }

        [Route("AppointmentEdit")]
        [HttpGet]
        public IActionResult AppointmentEdit(int? id)
        {
            var appointmentDetailsViewModel = new AppointmentDetailsViewModel
            {
                Appointment = _handlerAppointment.GetAppointment((int)id, _userManager.GetUserId(User)),
                Title = "Appointment Edit"
            };
            return View(appointmentDetailsViewModel.Appointment);
        }

        [Route("AppointmentEdit")]
        [HttpPost]
        public IActionResult AppointmentEdit(Appointment appointment)
        {
            if (!ModelState.IsValid) return View();
            var appointmentUpdated = _handlerAppointment.UpdateAppointment(appointment, _userManager.GetUserId(User));
            return RedirectToAction("AppointmentDetails", new { id = appointmentUpdated.Id });

        }
    }
}
