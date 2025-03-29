using Lawoffice.DTOs;
using Lawoffice.Services.TypeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lawoffice.Controllers
{
    [Authorize]
    public class CaseTypesController : Controller
    {
        private readonly ITypeService _typeService;

        public CaseTypesController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public async Task<IActionResult> Index()
        {
            var caseTypes = await _typeService.GetAllCaseTypesAsync();
            return View(caseTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypeRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _typeService.AddCaseTypeAsync(request);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var caseType = await _typeService.GetCaseTypeByIdAsync(id);
            if (caseType == null)
                return NotFound();

            var request = new TypeRequest
            {
                id = caseType.Id,
                name = caseType.TypeName
            };

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TypeRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _typeService.UpdateCaseTypeAsync(request);
            return RedirectToAction(nameof(Index));
        }
    }
}
