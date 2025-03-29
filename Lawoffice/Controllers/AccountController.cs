using Lawoffice.DTOs;
using Lawoffice.Services.AccountService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lawoffice.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            // Here, validate the user credentials (assumed to be done elsewhere)
            if (IsValidUser(request))
            {
                // Get claims for the user
                var claims = _accountService.GetUserClaims(request);

                // Create the claims identity
                var claimsIdentity = new ClaimsIdentity(claims, "admin");

                var principal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("admin", principal);
                // Redirect to admin home
                return RedirectToAction("Index", "Cases");
            }

            // If credentials are invalid
            ModelState.AddModelError("message", "Invalid login attempt.");
            return View(request);
        }

        private bool IsValidUser(LoginRequest request)
        {
            // Replace this with actual validation logic
            // For demonstration purposes, assuming valid credentials
            return request.username == "mohammedhashem" && request.password == "password123";
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("admin");
            return RedirectToAction("Login", "Account");
        }
    }
}
