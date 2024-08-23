using System;
using Domain.Helpers.Enums;
using HarBourBackEnd.Helpers.Enums;
using HarBourBackEnd.ViewModels.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _reservationService.GetAll();
            var reservations = datas.Select(m => new ReservationVM { Id = m.Id, UserEmail = m.UserEmail, OrderStatus = m.OrderStatus, YachtName = m.Yacht.Name }).ToList();
            return View(reservations);

        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Accept(int? id)
        {
            if (id is null) return BadRequest();
            var existReserv = await _reservationService.GetById((int)id);
            if (existReserv is null) return NotFound();

            existReserv.OrderStatus = OrderStatus.Accepted;



            await _reservationService.Edit((int)id,existReserv);
            return RedirectToAction("Index");
        }


        //public void SendEmail(string to, string subject, string html, string from = null)
        //{
        //    // create message
        //    var email = new MimeMessage();
        //    email.From.Add(MailboxAddress.Parse(from ?? _appSettings.From));
        //    email.To.Add(MailboxAddress.Parse(to));
        //    email.Subject = subject;
        //    email.Body = new TextPart(TextFormat.Html) { Text = html };

        //    // send email
        //    using var smtp = new SmtpClient();
        //    smtp.Connect(_appSettings.Server, _appSettings.Port, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(_appSettings.Username, _appSettings.Password);
        //    smtp.Send(email);
        //    smtp.Disconnect(true);
        //}




        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Reject(int? id)
        {
            if (id is null) return BadRequest();
            var existReserv = await _reservationService.GetById((int)id);
            if (existReserv is null) return NotFound();

            existReserv.OrderStatus = OrderStatus.Rejected;

            await _reservationService.Edit((int)id, existReserv);
            return RedirectToAction("Index");
        }
    
    }

}

