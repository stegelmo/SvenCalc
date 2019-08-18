using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SvenCalc.Enums;
using SvenCalc.Models;

namespace SvenCalc.Controllers
{
    public class HomeController : Controller
    {
        // *= Kör med Post-Redirect-Get (PRG-mönster) för detta exempel för att undvika multipla Post vid refresh och tillbaka-steg i webbläsare.
        // Inte för att det spelar så stor roll för en liten minräknare (Relevant om data skulle lagras/hanteras och i så fall flera gånger)
        // I en lite större tillämning hade jag kört på AJAX-anrop istället för att undvika "repost-meddelandet". 
        // (Se tex verktygen jag gjort på sajten skogskunskap.se. Exempel på ett verktyg: https://www.skogskunskap.se/rakna-med-verktyg/ekonomi/skogsvard-och-ekonomi/)
        // Jag vill dock inte krångla till det får mycket för denna uppgift och håller mig till "vanlig" form-post.

        // *
        private static string prgKey = "HomePRG";

        [HttpGet]
        public IActionResult Index()
        {
            CalcViewModel model = new CalcViewModel();

            // *
            if (TempData[prgKey] != null)
                model = JsonConvert.DeserializeObject<CalcViewModel>(TempData[prgKey].ToString());

            PopulateDDLs(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(CalcViewModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.OperatorType)
                {
                    case OperatorType.Addition:
                        model.Result = model.NumberA + model.NumberB;
                        break;
                    case OperatorType.Subtraction:
                        model.Result = model.NumberA - model.NumberB;
                        break;
                    case OperatorType.Division:
                        // Verkar gå bra med 'oändligt' svar => Bortkommenterad test av nämnare = 0
                        //if (model.NumberB == 0)
                        //    ModelState.AddModelError("NumberB", "Det går inte att dividera med 0.");
                        //else
                        model.Result = model.NumberA / model.NumberB;
                        break;
                    case OperatorType.Multiplication:
                        model.Result = model.NumberA * model.NumberB;
                        break;
                }

                // *
                TempData[prgKey] = JsonConvert.SerializeObject(model);
                return RedirectToAction("Index");
            }

            PopulateDDLs(model);
            return View(model);
        }

        private void PopulateDDLs(CalcViewModel model)
        {
            model.ValidOperators = new List<OperatorType>()
            {
                OperatorType.Addition,
                OperatorType.Subtraction,
                OperatorType.Multiplication,
                OperatorType.Division
            };
        }
    }
}

