using Azure.Core;
using Lawoffice.DTOs;
using Lawoffice.Models.LawOfficeModels;
using Lawoffice.Services.SessionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lawoffice.Controllers
{
    [Authorize]
    public class SessionsController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionsController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        // ✅ List sessions by case ID
        public async Task<IActionResult> Index(int caseId)
        {
            var sessions = await _sessionService.GetSessionsByCaseIdAsync(caseId);
            ViewBag.CaseId = caseId;
            return View(sessions);
        }

        // ✅ GET: Create
        public IActionResult Create(int caseId)
        {
            var model = new SessionRequest { case_id = caseId };
            return View(model);
        }

        // ✅ POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _sessionService.AddSessionAsync(request);
            return RedirectToAction("Index", new { caseId = request.case_id });
        }

        // ✅ GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);
            if (session == null) return NotFound();

            var model = new SessionRequest
            {
                id = session.Id,
                case_id = session.CaseId??0,
                session_date = session.SessionDate,
                description = session.Description,
                descision = session.Descision
            };

            return View(model);
        }

        // ✅ POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SessionRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _sessionService.UpdateSessionAsync(request);
            return RedirectToAction("Index", new { caseId = request.case_id });
        }

        // ✅ POST: Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);
            if (session == null) return NotFound();

            var caseId = session.CaseId;
            await _sessionService.DeleteSessionAsync(id);
            return RedirectToAction("Index", new { caseId = caseId });
        }
    }
}
