using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using HarBourBackEnd.ViewModels.Complaints;
using System.Xml.Linq;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ComplaintSuggestController : Controller
    {
        private readonly IComplaintService _complaintService;

        public ComplaintSuggestController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var complaints = await _complaintService.GetAll();

            var datas = complaints.Select(m => new ComplaintVM { Id = m.Id, UserEmail = m.UserEmail, UserPhone=m.UserPhone, Subject=m.Subject, UserFullName = m.UserFullName, UserSuggest = m.UserSuggest });

            return View(datas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var data = await _complaintService.GetById((int)id);
            if (data is null) return NotFound();

            await _complaintService.Delete(data);
            return RedirectToAction(nameof(Index));
        }



    }
}

