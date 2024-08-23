using System;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using HarBourBackEnd.ViewModels.User;
using Service.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(IAccountService accountService,
                              UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            _accountService = accountService;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<UserVM> userRoles = new();

            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var roles = await _accountService.GetRoles(user);
                string rolesStr = string.Join(",", roles);

                userRoles.Add(new UserVM
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Roles = rolesStr,
                    UserId = user.Id,
                    UserRoles = roles.ToList()

                });
            }
            return View(userRoles);
        }


        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddRole()
        {
            ViewBag.users = new SelectList(_userManager.Users.ToList(), "Id", "FullName");
            ViewBag.roles = new SelectList(_roleManager.Roles.ToList(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(AddRoleVM request)
        {
            var user = _userManager.FindByIdAsync(request.UserId).Result;
            var role = _roleManager.FindByIdAsync(request.RoleId).Result;

            await _userManager.AddToRoleAsync(user, role.ToString());
            return RedirectToAction("Index");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRole(RemoveRoleVM request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Count <= 1)
            {
                TempData["Error"] = "Cannot remove the last remaining role";
                return RedirectToAction("Index");
            }

            var role = await _roleManager.FindByNameAsync(request.RoleName);

            if (role != null)
            {
                await _userManager.RemoveFromRoleAsync(user, request.RoleName);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetRoleCount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Json(new { roleCount = 0 });
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Json(new { roleCount = roles.Count });
        }
    }
}

