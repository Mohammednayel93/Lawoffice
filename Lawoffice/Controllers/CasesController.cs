using Lawoffice.Models;
using Lawoffice.Models.LawOfficeModels;
using Lawoffice.Services.CaseService;
using Lawoffice.Services.ProcedureService;
using Lawoffice.Services.SessionService;
using Lawoffice.Services.TypeService;
using Lawoffice.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lawoffice.Controllers
{
    [Authorize]
    public class CasesController : Controller
    {
        private readonly ICaseService _caseService;
        private readonly IUserService _userService;
        private readonly ITypeService _typeService;
        private readonly IProcedureService _procedureService;
        private readonly ISessionService _sessionService;
        private readonly IWebHostEnvironment _env;

       

        public CasesController(
            ICaseService caseService,
            IUserService userService,
            ITypeService typeService,
            IProcedureService procedureService,
            ISessionService sessionService,
            IWebHostEnvironment env)
        {
            _caseService = caseService;
            _userService = userService;
            _typeService = typeService;
            _procedureService = procedureService;
            _sessionService = sessionService;
            _env = env;
        }


        // GET: /Cases
        public async Task<IActionResult> Index(int page = 1, string? caseNumber = null, string? courtName = null, int? clientId = null, int? opponentId = null)
        {
            var cases = await _caseService.GetCasesAsync(page, 20, caseNumber, courtName, clientId, opponentId);

            // Populate dropdowns using services
            var clients = await _userService.GetAllUsersListAsync();
            var opponents = await _userService.GetAllUsersListAsync(); // Can be same unless separated
            var caseTypes = await _typeService.GetAllCaseTypesAsync();

            ViewBag.Clients = new SelectList(clients, "Id", "Name", clientId);
            ViewBag.Opponents = new SelectList(opponents, "Id", "Name", opponentId);
 
            ViewBag.CourtName = courtName;
            ViewBag.CaseNumber = caseNumber;

            return View(cases);
        }


        // GET: /Cases/Create
        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }


        // POST: /Cases/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CaseRequest request)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(request.client_id, request.opponent_id, request.case_type_id);
                return View(request);

            }

            await _caseService.AddCaseAsync(request);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var caseItem = await _caseService.GetCaseByIdAsync(id);
            if (caseItem == null) return NotFound();

            var request = new CaseRequest
            {
                id = caseItem.Id,
                client_id = caseItem.ClientId ?? 0,
                opponent_id = caseItem.OpponentId ?? 0,
                description = caseItem.Description,
                case_type_id = caseItem.CaseTypeId,
                filing_lawsuit_date = caseItem.FilingLawsuitDate,
                court_name = caseItem.CourtName,
                lawsuit_number = caseItem.LawsuitNumber,
                power_of_attorney_number = caseItem.PowerOfAttorneyNumber,
                fees = caseItem.Fees,
                fees_payment = caseItem.PaymentOfFees,
             };

            await PopulateDropdowns(request.client_id, request.opponent_id, request.case_type_id);
            return View(request);
        }

        // POST: /Cases/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CaseRequest request)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(request.client_id, request.opponent_id, request.case_type_id);
                return View(request);

            }

            await _caseService.UpdateCaseAsync(request);
            return RedirectToAction(nameof(Index));
        }
        private async Task PopulateDropdowns(int? selectedClient = null, int? selectedOpponent = null, int? selectedType = null)
        {
            var clients = await _userService.GetAllUsersListAsync();
            var opponents = await _userService.GetAllUsersListAsync(); // or a different source
            var caseTypes = await _typeService.GetAllCaseTypesAsync();

            ViewBag.Clients = new SelectList(clients, "Id", "Name", selectedClient);
            ViewBag.Opponents = new SelectList(opponents, "Id", "Name", selectedOpponent);
            ViewBag.CaseTypes = new SelectList(caseTypes, "Id", "TypeName", selectedType);
        }

        // POST: /Cases/UploadFiles
        [HttpPost]
        public async Task<IActionResult> UploadFiles(int caseId,int fileTypeId, List<IFormFile> files)
        {
            if (files != null && files.Count > 0)
            {
                await _caseService.UploadCaseFilesAsync(caseId, fileTypeId, files);
            }

            return RedirectToAction("Edit", new { id = caseId });
        }
        public async Task<IActionResult> GetProceduresPartial(int id)
        {
            var procedures = await _procedureService.GetProceduresByCaseIdAsync(id);
            return PartialView("_ProceduresPartial", procedures);
        }

        public async Task<IActionResult> GetSessionsPartial(int id)
        {
            var sessions = await _sessionService.GetSessionsByCaseIdAsync(id);
            return PartialView("_SessionsPartial", sessions);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProcedure(int id)
        {
            await _procedureService.DeleteProcedureAsync(id);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSession(int id)
        {
            await _sessionService.DeleteSessionAsync(id);
            return Json(new { success = true });
        }
        public async Task<IActionResult> Files(int id)
        {
            var caseItem = await _caseService.GetCaseByIdAsync(id);
            var fileTypes = await _typeService.GetAllFileTypesAsync(); // Assuming this returns list of file types
            ViewBag.FileTypes = new SelectList(fileTypes, "Id", "TypeName");
            return View(caseItem);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var file = await _caseService.GetCaseFileByIdAsync(id);
            if (file == null) return NotFound();

            var caseId = file.CaseId;
            await _caseService.DeleteCaseFileAsync(id);
            return RedirectToAction("Files", new { id = caseId });
        }

        public async Task<IActionResult> DownloadFile(int id)
        {
            var file = await _caseService.GetCaseFileByIdAsync(id);
            if (file == null) return NotFound();

            var path = Path.Combine(_env.WebRootPath, file.FileUrl.TrimStart('/'));
            if (!System.IO.File.Exists(path)) return NotFound();

            var mime = "application/octet-stream"; // you can detect type if needed
            return PhysicalFile(path, mime, file.Case?.LawsuitNumber+"-"+file.Id);
        }

    }
}
