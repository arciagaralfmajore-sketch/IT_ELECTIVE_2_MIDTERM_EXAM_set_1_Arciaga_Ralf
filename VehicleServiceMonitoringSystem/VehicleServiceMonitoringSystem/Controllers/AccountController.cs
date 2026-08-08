using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VehicleServiceMonitoringSystem.DTOs;
using VehicleServiceMonitoringSystem.Repositories;

namespace VehicleServiceMonitoringSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // =========================
        // LOGIN - GET
        // =========================

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // =========================
        // LOGIN - POST
        // =========================

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var user = _userRepository.GetByUsername(dto.Username);

            if (user == null || user.Password != dto.Password)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Invalid username or password.");

                return View(dto);
            }

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.Username),

                new Claim(
                    ClaimTypes.GivenName,
                    user.FirstName),

                new Claim(
                    ClaimTypes.Surname,
                    user.LastName),

                new Claim(
                    ClaimTypes.Email,
                    user.Email)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToAction(
                "Index",
                "ServiceJob");
        }

        // =========================
        // REGISTER - GET
        // =========================

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // =========================
        // REGISTER - POST
        // =========================

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegistrationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var existingUser =
                _userRepository.GetByUsername(dto.Username);

            if (existingUser != null)
            {
                ModelState.AddModelError(
                    "Username",
                    "Username is already taken.");

                return View(dto);
            }

            var user = new Models.User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Username = dto.Username,
                Password = dto.Password
            };

            _userRepository.Add(user);

            TempData["SuccessMessage"] =
                "Registration successful. Please log in.";

            return RedirectToAction(nameof(Login));
        }

        // =========================
        // LOGOUT
        // =========================

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Login));
        }
    }
}