using Lawoffice.Models;
using Lawoffice.Models.LawOfficeModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.EF;
using File = Lawoffice.Models.LawOfficeModels.File;

namespace Lawoffice.Services.CaseService
{
    public class CaseService : ICaseService
    {
        private readonly LawOfficeContext _context;
        private readonly IWebHostEnvironment _env;

        public CaseService(LawOfficeContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IPagedList<Case>> GetCasesAsync(int page = 1, int pageSize = 20,
                                                         string? caseNumber = null,
                                                          string? courtName = null,
                                                          int? clientId = null,
                                                          int? opponentId = null)
        {
            var query = _context.Cases
                .Include(c => c.Client)
                .Include(c => c.Opponent)
                .Include(c => c.CaseType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(caseNumber))
                query = query.Where(c => c.LawsuitNumber == caseNumber);

            if (!string.IsNullOrWhiteSpace(courtName))
                query = query.Where(c => c.CourtName.Contains(courtName));

            if (clientId.HasValue)
                query = query.Where(c => c.ClientId == clientId);

            if (opponentId.HasValue)
                query = query.Where(c => c.OpponentId == opponentId);

            return await query.OrderByDescending(c => c.Id).ToPagedListAsync(page, pageSize);
        }

        public async Task<Case?> GetCaseByIdAsync(int id)
        {
            return await _context.Cases
                .Include(c => c.CaseType)
                .Include(c => c.Client)
                .Include(c => c.Opponent)
                .Include(c => c.Procedures)
                .Include(c => c.Files)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCaseAsync(CaseRequest request)
        {
            var newCase = new Case
            {
                ClientId = request.client_id,
                OpponentId = request.opponent_id,
                Description = request.description,
                CaseTypeId = request.case_type_id,
                FilingLawsuitDate = request.filing_lawsuit_date,
                CourtName = request.court_name,
                LawsuitNumber = request.lawsuit_number,
                PowerOfAttorneyNumber = request.power_of_attorney_number,
                Fees = request.fees,
                PaymentOfFees = request.fees_payment,
                CreatedAt=DateTime.Now,
                IsActive=true,
                UpdatedAt=DateTime.Now,
                
             };

            _context.Cases.Add(newCase);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCaseAsync(CaseRequest request)
        {
            var existingCase = await _context.Cases.FindAsync(request.id);
            if (existingCase == null) return;

            existingCase.ClientId = request.client_id;
            existingCase.OpponentId = request.opponent_id;
            existingCase.Description = request.description;
            existingCase.CaseTypeId = request.case_type_id;
            existingCase.FilingLawsuitDate = request.filing_lawsuit_date;
            existingCase.CourtName = request.court_name;
            existingCase.LawsuitNumber = request.lawsuit_number;
            existingCase.PowerOfAttorneyNumber = request.power_of_attorney_number;
            existingCase.Fees = request.fees;
            existingCase.PaymentOfFees = request.fees_payment;
            existingCase.UpdatedAt = DateTime.Now;
 
            _context.Cases.Update(existingCase);
            await _context.SaveChangesAsync();
        }

        public async Task UploadCaseFilesAsync(int caseId,int fileTypeId, List<IFormFile> files)
        {
            var caseEntity = await _context.Cases.FindAsync(caseId);
            if (caseEntity == null) return;

            var uploadDir = Path.Combine(_env.WebRootPath, "uploads", "cases", caseId.ToString());
            Directory.CreateDirectory(uploadDir);

            foreach (var file in files)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _context.Files.Add(new Models.LawOfficeModels.File
                {
                    CaseId = caseId,
                    FileUrl = $"/uploads/cases/{caseId}/{fileName}",
                    FileTypeId =fileTypeId,
                    IsActive=true,
                    CretaedAt=DateTime.Now,
                    UpdatedAt=DateTime.Now,
                    Type=1
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCaseFileAsync(int fileId)
        {
            var file = await _context.Files.FindAsync(fileId);
            if (file == null) return;

            // Optional: Delete the physical file from the server
            var filePath = Path.Combine(_env.WebRootPath, file.FileUrl.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            // Remove from database
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();
        }
        public async Task<File?> GetCaseFileByIdAsync(int id)
        {
            return await _context.Files
                .Include(f => f.FileType)
                .Include(m=>m.Case)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

    }
}
