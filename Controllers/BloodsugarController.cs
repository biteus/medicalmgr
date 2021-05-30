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
    public class BloodsugarController : Controller
    {
        private readonly ILogger<BloodsugarController> _logger;
        private readonly IBloodSugar _handlerBloodSugar;
        private readonly UserManager<User> _userManager;

        public BloodsugarController(IBloodSugar repositoryBloodSugar, UserManager<User> userManager, ILogger<BloodsugarController> logger)
        {
            this._logger = logger;
            this._handlerBloodSugar = repositoryBloodSugar;
            this._userManager = userManager;
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

        [Route("BSCreate")]
        [HttpGet]
        public IActionResult BSCreate()
        {
            return View();
        }

        [Route("BSCreate")]
        [HttpPost]
        public IActionResult BSCreate(BloodSugar bs)
        {
            if (!ModelState.IsValid) return View();
            var userID = _userManager.GetUserId(User);
            var bloodSugarAdded = _handlerBloodSugar.AddBloodSugar(bs, userID);
            return RedirectToAction("details", new { id = bloodSugarAdded.Id });

        }

        //[Route("Delete")]
        //public IActionResult Delete(int? id)
        //{
        //    var homeDetailsViewModel = new HomeDetailsViewModel
        //    {   
        //        Medication = _handlerBloodPressure.DeleteBloodPressure(id ?? 1, _userManager.GetUserId(User)),
        //        Title = "Medication Delete"
        //    };
        //    return View(BPDetailsViewModel);
        //}


        [Route("BSList")]
        public IActionResult BSList()
        {
            var modelBloodSugarListView = new BloodSugarListViewModel
            {
                BloodSugars = _handlerBloodSugar.GetAllBloodSugar(_userManager.GetUserId(User)),
                Title = "Blood Sugar Readings"
            };
            return View(modelBloodSugarListView);
        }

        ////[Route("Edit")]
        ////public IActionResult Edit(int? id)
        ////{
        ////    if (!ModelState.IsValid) return View();
        ////    var medicationUpdated = _handlerMedication.UpdateMedication((int) id, _userManager.GetUserId(User));
        ////    return RedirectToAction("Details", new { id = medicationUpdated.Id });
        ////}

        //[Route("Edit")]
        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    var homeDetailsViewModel = new HomeDetailsViewModel
        //    {
        //        Medication = _handlerMedication.GetMedication((int)id, _userManager.GetUserId(User)),
        //        Title = "Medication Edit"
        //    };
        //    return View(homeDetailsViewModel.Medication);
        //}

        //[Route("Edit")]
        //[HttpPost]
        //public IActionResult Edit(Medication medication)
        //{
        //    if (!ModelState.IsValid) return View();
        //    var medicationUpdated = _handlerMedication.UpdateMedication(medication, _userManager.GetUserId(User));
        //    return RedirectToAction("Details", new { id = medicationUpdated.Id });

        //}

        //[Route("Details")]
        //public IActionResult Details(int? id)
        //{
        //    var homeDetailsViewModel = new HomeDetailsViewModel
        //    {
        //        Medication = _handlerMedication.GetMedication((int)id, _userManager.GetUserId(User)),
        //        Title = "Medication Details"
        //    };
        //    return View(homeDetailsViewModel);
        //}
    }
}
