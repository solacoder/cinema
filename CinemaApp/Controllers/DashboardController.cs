using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CinemaApp.Service.Abstract;
using System.Collections;

namespace CinemaApp.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Dashboard")]
    public class DashboardController : Controller
    {
        private readonly IDashBoardService service;

        public DashboardController(IDashBoardService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetDashBoardData()
        {
            Hashtable table = service.GetDashBoardData();

            var data = new
            {
                CinemaOwners = table["CinemaOwners"],
                Cinemas = table["Cinemas"],
                Screens = table["Screens"],
                MovieCategories = table["MovieCategories"],
                Movies = table["Movies"],
                ShowTimes = table["ShowTimes"]
            };

            return Json(data);
        }
    }
}