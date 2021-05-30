using MedicalManager.Models;
using MedicalManager.Models.Interfaces;
using MedicalManager.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

using MedicalManager;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MedicalManager.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IBloodSugar _handlerBloodSugar;
        private readonly IBloodPressure _handlerBloodPressure;
        private readonly UserManager<User> _userManager;

        //private readonly IConfiguration _configuration;
        public IConfiguration Configuration { get; }
        //IdentityUser user = _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        //public HomeController(IMedication repositoryMedication, ILogger<HomeController> logger)
        //{
        //    this._logger = logger;
        //    this._handlerMedication = repositoryMedication;
        //}
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public DashboardController(IBloodSugar repositoryBloodSugar, IBloodPressure repositoryBloodPressure, UserManager<User> userManager, ILogger<DashboardController> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._handlerBloodSugar = repositoryBloodSugar;
            this._handlerBloodPressure = repositoryBloodPressure;
            this._userManager = userManager;
            this.Configuration = configuration;
        }

        

        //[AllowAnonymous]
        public IActionResult Index()
        {
            var userID = _userManager.GetUserId(User);
            if (!string.IsNullOrEmpty(userID)){
                var model = _handlerBloodSugar.GetAllBloodSugar(userID);
                return View(model);
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

        [Route("BSDashboard")]
        [HttpGet]
        public IActionResult BSDashboard()
        {
            
            var bsDashboardViewModel = new BloodSugarListViewModel();

            var userID = _userManager.GetUserId(User);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var model = _handlerBloodSugar.GetAllBloodSugar(userID);
            if (!string.IsNullOrEmpty(userID))
            {
                // var bpDashboardViewModel = new BloodPressureListViewModel();
                //var model = _handlerBloodPressure.GetAllBloodPressure(userID);
                var lstIntSugar = new List<int>() { };
                var lstStrDateTime = new List<string>() { };

                foreach (var bs in model)
                {
                    lstIntSugar.Add(bs.Sugar);
                    lstStrDateTime.Add(bs.ReadingDate.ToString());

                }
                bsDashboardViewModel.Sugars = lstIntSugar;
                bsDashboardViewModel.ReadingTime= lstStrDateTime;

                return View(bsDashboardViewModel);
            }

            return View();
        }



        [Route("BPDashboard")]
        [HttpGet]
        public IActionResult BPDashboard()
        {
            var bpDashboardViewModel = new BloodPressureListViewModel();

            var userID = _userManager.GetUserId(User);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            var model = _handlerBloodPressure.GetAllBloodPressure(userID);
            if (!string.IsNullOrEmpty(userID))
            {
                // var bpDashboardViewModel = new BloodPressureListViewModel();
                //var model = _handlerBloodPressure.GetAllBloodPressure(userID);
                var lstIntSystolic = new List<int>() { };
                var lstIntDiastolic = new List<int>() { };
                var lstStrDateTime = new List<string>() { };

                foreach (var bp in model)
                {
                    lstIntSystolic.Add(bp.Systolic);
                    lstIntDiastolic.Add(bp.Diastolic);
                    lstStrDateTime.Add(bp.ReadingDate.ToString());

                }
                bpDashboardViewModel.Systolic = lstIntSystolic;
                bpDashboardViewModel.Diastolic = lstIntDiastolic;
                bpDashboardViewModel.ReadingTime = lstStrDateTime;

                return View(bpDashboardViewModel);
            }

            return View();
        }


        //[Route("BPDashboard")]
        //[HttpPost]
        ////public IActionResult BPDashboard()
        //public JsonResult AjaxMethod()
        //{
        //    var bpDashboardViewModel = new BloodPressureListViewModel();

        //    var userID = _userManager.GetUserId(User);
        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //        WriteIndented = true
        //    };
        //    var model = _handlerBloodPressure.GetAllBloodPressure(userID);
        //    if (!string.IsNullOrEmpty(userID))
        //    {
        //       // var bpDashboardViewModel = new BloodPressureListViewModel();
        //        //var model = _handlerBloodPressure.GetAllBloodPressure(userID);
        //        var lstIntSystolic = new List<int>() { };
        //        var lstIntDiastolic = new List<int>() { };
        //        var lstStrDateTime = new List<string>() { };

        //        foreach (var bp in model)
        //        {
        //            lstIntSystolic.Add(bp.Systolic);
        //            lstIntDiastolic.Add(bp.Diastolic);
        //            lstStrDateTime.Add(bp.ReadingDate.ToString());

        //        }
        //        bpDashboardViewModel.Systolic = lstIntSystolic;
        //        bpDashboardViewModel.Diastolic = lstIntDiastolic;
        //        bpDashboardViewModel.ReadingTime = lstStrDateTime;
        //    }
        //    //return JsonSerializer.Serialize<BloodPressureListViewModel>(bpDashboardViewModel, options);
        //    return Json(bpDashboardViewModel);
        //}

        //    [Route("BPDashboard")]
        //    [HttpPost]
        //    public IActionResult RetainBloodpressureData()
        //    {
        //        var userID = _userManager.GetUserId(User);


        //        if (!string.IsNullOrEmpty(userID))
        //        {
        //            var bpDashboardViewModel = new BloodPressureListViewModel();
        //            var model = _handlerBloodPressure.GetAllBloodPressure(userID);
        //            var lstIntSystolic = new List<int>() { };
        //            var lstIntDiastolic = new List<int>() { };
        //            var lstStrDateTime = new List<string>() { };

        //            foreach (var bp in model)
        //            {
        //                lstIntSystolic.Add(bp.Systolic);
        //                lstIntDiastolic.Add(bp.Diastolic);
        //                lstStrDateTime.Add(bp.ReadingDate.ToString());

        //            }
        //            bpDashboardViewModel.Systolic = lstIntSystolic;
        //            bpDashboardViewModel.Diastolic = lstIntDiastolic;
        //            bpDashboardViewModel.ReadingTime = lstStrDateTime;

        //            return View(bpDashboardViewModel);
        //        }
        //        return View();
        //    }
        //}


        //public Json BPDashboardData()
        //{
        //    try {

        //        string connectionString = @"Server=(localdb)\\mssqllocaldb;Database=MedicalManager;Trusted_Connection=True;MultipleActiveResultSets=true";
        //        List<int> Systolic = new List<int>() { };
        //        List<int> Diastolic = new List<int>() { };
        //        List<string> ReadingTime = new List<string>() { };

        //        SqlConnection con = new SqlConnection(connectionString);
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("Select Systolic, Diastolic, ReadingDate From BloodPressures", con);
        //        DataTable dt = new DataTable();
        //        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        //        sqlDataAdapter.Fill(dt);

        //        if(dt.Rows.Count == 0)
        //        {
        //            Systolic[0] = 0;
        //            Diastolic[0] = 0;
        //            ReadingTime[0] = "0";
        //        } 
        //        else
        //        {
        //            Systolic = (List<int>)dt.Rows[0]["Systolic"];
        //            Diastolic = (List<int>)dt.Rows[0]["Diastolic"];
        //            ReadingTime = (List<string>)dt.Rows[0]["ReadingDate"];
        //        }



        //    }
        //    catch (Exception ex) 
        //    {
        //        throw ex;
        //    }
    }
}
