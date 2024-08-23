using System;
using HarBourBackEnd.Helpers.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class DashboardController : Controller
	{
        public IActionResult Index()
        {
            return View();
        }
    }
}

