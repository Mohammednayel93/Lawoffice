using Lawoffice.DTOs;
using Lawoffice.Services.TypeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lawoffice.Controllers
{
    [Authorize]
    public class FileTypesController : Controller
    {
        private readonly ITypeService _typeService;

        public FileTypesController(ITypeService typeService)
        {
            _typeService = typeService;
        }

        public async Task<IActionResult> Index()
        {
            var fileTypes = await _typeService.GetAllFileTypesAsync();
            return View(fileTypes);
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

            await _typeService.AddFileTypeAsync(request);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var fileType = await _typeService.GetFileTypeByIdAsync(id);
            if (fileType == null)
                return NotFound();

            var request = new TypeRequest
            {
                id = fileType.Id,
                name = fileType.TypeName
            };

            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TypeRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _typeService.UpdateFileTypeAsync(request);
            return RedirectToAction(nameof(Index));
        }
    }
}
