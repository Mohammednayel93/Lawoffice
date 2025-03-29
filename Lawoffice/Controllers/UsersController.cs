using Lawoffice.DTOs;
using Lawoffice.Models;
using Lawoffice.Models.LawOfficeModels;
using Lawoffice.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lawoffice.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /Users
        public async Task<IActionResult> Index(int page = 1, string keyword = "")
        {
            var users = await _userService.GetAllUsersAsync(page, 20, keyword);
            ViewBag.keyword = keyword;
            return View(users);
        }

        // GET: /Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: /Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            // Check if phone already exists
            bool phoneExists = await _userService.IsPhoneExistsAsync(request.phone_number_1);
            if (phoneExists)
            {
                ModelState.AddModelError("Phone", "Phone number already exists.");
                return View(request);
            }

            await _userService.AddUserAsync(request);
            return RedirectToAction(nameof(Index));
        }


        // GET: /Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var request = new UserRequest
            {
                id = user.Id,
                name = user.Name,
                phone_number_1 = user.PhoneNumber1,
                email = user.Email,
                identity_number = user.IdentityNumber,
                
                // Map other fields as needed
            };

            return View(request);
        }

        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            // Check if phone number exists (excluding this user's own number)
            bool phoneExists = await _userService.IsPhoneExistsAsync(request.phone_number_1, request.id);
            if (phoneExists)
            {
                ModelState.AddModelError("Phone", "Phone number already exists.");
                return View(request);
            }

            await _userService.UpdateUserAsync(request);
            return RedirectToAction(nameof(Index));
        }


        // Optional: AJAX endpoint to check if phone exists
        [HttpPost]
        public async Task<JsonResult> IsPhoneExists(string phone, int? userId)
        {
            bool exists = await _userService.IsPhoneExistsAsync(phone, userId);
            return Json(!exists); // For client-side validation: true = valid
        }
    }
}
