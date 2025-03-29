using Lawoffice.DTOs;
using Lawoffice.Models.LawOfficeModels;
using Lawoffice.Services.ProcedureService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lawoffice.Controllers
{
    [Authorize]
    public class ProceduresController : Controller
    {
        private readonly IProcedureService _procedureService;

        public ProceduresController(IProcedureService procedureService)
        {
            _procedureService = procedureService;
        }

        // ✅ List all procedures for a case
        public async Task<IActionResult> Index(int caseId)
        {
            var procedures = await _procedureService.GetProceduresByCaseIdAsync(caseId);
            ViewBag.CaseId = caseId;
            return View(procedures);
        }

        // ✅ GET: Create
        public IActionResult Create(int caseId)
        {
            var model = new ProcedureRequest { case_id = caseId };
            return View(model);
        }

        // ✅ POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProcedureRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _procedureService.AddProcedureAsync(request);
            return RedirectToAction("Index", new { caseId = request.case_id });
        }

        // ✅ GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var procedure = await _procedureService.GetByIdAsync(id);
            if (procedure == null) return NotFound();

            var model = new ProcedureRequest
            {
                id = procedure.Id,
                case_id = procedure.CaseId ?? 0,
                procedure_date = procedure.ProcedureDate,
                decription = procedure.Description
            };

            return View(model);
        }

        // ✅ POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProcedureRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _procedureService.UpdateProcedureAsync(request);
            return RedirectToAction("Index", new { caseId = request.case_id });
        }

        // ✅ POST: Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var procedure = await _procedureService.GetByIdAsync(id);
            if (procedure == null) return NotFound();

            var caseId = procedure.CaseId;
            await _procedureService.DeleteProcedureAsync(id);
            return RedirectToAction("Index", new { caseId = caseId });
             
        }
    }
}
