using System;
using System.Net.Mail;
using Domain.Helpers.Enums;
using Domain.Models;
using HarBourBackEnd.Helpers;
using HarBourBackEnd.ViewModels.Order;
using HarBourBackEnd.ViewModels.Yacht;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Repository.Data;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
    public class YachtDetailController : Controller
    {
        private readonly IYachtService _yachtService;
        private readonly IReservationService _reservation;
        private readonly UserManager<AppUser> _manager;
        private readonly AppDbContext _appDbContext;

        public YachtDetailController(IYachtService yachtService, IReservationService reservation, UserManager<AppUser> manager, AppDbContext appDbContext)
        {
            _yachtService = yachtService;
            _reservation = reservation;
            _manager = manager;
            _appDbContext = appDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            var existYacht = await _yachtService.GetById((int)id);

            if (existYacht is null) return NotFound();

            YachtDetailVM yacht = new()
            {
                Id = (int)id,
                Build = existYacht.Build,
                Description = existYacht.Description,
                Guest = existYacht.Guest,
                Information = existYacht.Information,
                Length = existYacht.Length,
                Name = existYacht.Name,
                Price = existYacht.Price,
                YachtCategory = existYacht.YachtCategory.Name,
                YachtImages = existYacht.YachtImages
            };

            var yachts = await _yachtService.GetAll();

            var reservDates = await _reservation.GetAll();


            YachtDetailPageVM model = new()
            {
                Yachts = yachts,
                YachtDetail = yacht,
                ReservDates = reservDates.Where(m => m.YachtId == (int)id && m.OrderStatus == OrderStatus.Accepted).Select(m => new ReservDatesVM { StartDate = m.StartDate.AddMonths(-1).ToString("yyyy,MM,dd"), EndDate = m.EndDate.AddMonths(-1).ToString("yyyy,MM,dd") }).ToList()
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> AddReservation(OrderVM request)
        {
            var yacht = await _yachtService.GetById(request.YachtId);
            var startDate = Convert.ToDateTime(request.StartDate);
            var endDate = Convert.ToDateTime(request.EndDate);

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _manager.FindByNameAsync(User.Identity.Name);

                if (request is not null)
                {
                    bool dublicate = _appDbContext.Reservations.Any(m =>
                        m.StartDate.Date.Day == startDate.Day &&
                        m.StartDate.Date.Month == startDate.Month &&
                        m.StartDate.Date.Year == startDate.Year &&
                        m.YachtId == yacht.Id &&
                        m.OrderStatus == Domain.Helpers.Enums.OrderStatus.Accepted);

                    if (dublicate is true)
                    {
                        return Problem("This dates have already reserved"); // Düzəliş: Yenidən `request` obyektini keçirik.
                    }
                    if(request.Guest > yacht.Guest)
                    {
                        return Problem($"You can only reserve maximum {yacht.Guest} persons");
                    }

                    Reservation reservation = new()
                    {
                        YachtId = yacht.Id,
                        AppUserId = user.Id,
                        EndDate = endDate,
                        StartDate = startDate,
                        UserName = request.UserName,
                        UserEmail = request.UserEmail,
                        Guests = request.Guest,
                        OrderStatus = Domain.Helpers.Enums.OrderStatus.Pending
                    };

                    _appDbContext.Reservations.Add(reservation);
                    _appDbContext.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }

            if (yacht is null) return NotFound();

            return Ok();
        }





      



    }
}

