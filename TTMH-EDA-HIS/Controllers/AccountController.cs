﻿using TTMH_EDA_HIS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TTMH_EDA_HIS.Data;
using TTMH_EDA_HIS.Interfaces;
using TTMH_EDA_HIS.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace TTMH_EDA_HIS.Controllers
{
    public class AccountController : Controller
    {
        private readonly HISDB.Data.HisdbContext _context;
        private readonly IHashService _hashService;
        public AccountController(HISDB.Data.HisdbContext context,IHashService hashService) 
        {
            _context = context;
            _hashService = hashService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "CPOEs");
			}
			else
			{
                return View();
            }
        }
		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(AccountLoginViewModel vm)
		{
			if (ModelState.IsValid)
            {
				//Employee? user = await _context.Employees.FirstOrDefaultAsync(x =>
				//	x.Account.ToUpper() == vm.Account.ToUpper()
				//	&&
				//	x.Password == _hashService.SHA512Hash(vm.Password)
				//);
				HISDB.Models.Employee? user = await _context.Employees.FirstOrDefaultAsync(x =>
					x.Account.ToUpper() == vm.Account.ToUpper()
					&&
					x.Password == vm.Password
				);
				if (user==null)
                {
					ModelState.AddModelError(string.Empty, "密碼或帳號無效！");
					return View(vm);
                }
				string role = "";
				if (await _context.Doctors.AnyAsync(x => x.DoctorId == user.EmployeeId)) 
				{
					role = "Doctor";
				}
				else if (await _context.Pharmacists.AnyAsync(x => x.PhaId == user.EmployeeId))
				{
					role = "Pharmacist";
				}
				else
				{
					role = "Employee";
				}

				List<Claim> claims = new List<Claim>()
				{
					new Claim(ClaimTypes.NameIdentifier,user.EmployeeId),
					new Claim(ClaimTypes.Name,user.Account),
					new Claim(ClaimTypes.GivenName,user.EmployeeName),
					new Claim(ClaimTypes.Role,role)
				};
				ClaimsIdentity identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
				AuthenticationProperties properties = new AuthenticationProperties() { };
				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(identity),
					properties
				);
				return RedirectToAction("ChartList", "CPOEs");
			};
			return View(vm);
		}
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}
		[HttpGet]
		public IActionResult Denied()
		{
			return View();
		}






















	}
}
