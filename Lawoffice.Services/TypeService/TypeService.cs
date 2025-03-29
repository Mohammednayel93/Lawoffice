using Lawoffice.DTOs;
using Lawoffice.Models.LawOfficeModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lawoffice.Services.TypeService
{
    public class TypeService : ITypeService
    {
        private readonly LawOfficeContext _context;

        public TypeService(LawOfficeContext context)
        {
            _context = context;
        }

        // -------- FILE TYPE --------

        public async Task<List<FileType>> GetAllFileTypesAsync()
        {
            return await _context.FileTypes.ToListAsync();
        }

        public async Task<FileType?> GetFileTypeByIdAsync(int id)
        {
            return await _context.FileTypes.FindAsync(id);
        }

        public async Task AddFileTypeAsync(TypeRequest request)
        {
            var fileType = new FileType
            {
                TypeName = request.name
            };

            _context.FileTypes.Add(fileType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFileTypeAsync(TypeRequest request)
        {
            var fileType = await _context.FileTypes.FindAsync(request.id);
            if (fileType == null) return;

            fileType.TypeName = request.name;
            _context.FileTypes.Update(fileType);
            await _context.SaveChangesAsync();
        }

        // -------- CASE TYPE --------

        public async Task<List<CaseType>> GetAllCaseTypesAsync()
        {
            return await _context.CaseTypes.ToListAsync();
        }

        public async Task<CaseType?> GetCaseTypeByIdAsync(int id)
        {
            return await _context.CaseTypes.FindAsync(id);
        }

        public async Task AddCaseTypeAsync(TypeRequest request)
        {
            var caseType = new CaseType
            {
                TypeName = request.name
            };

            _context.CaseTypes.Add(caseType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCaseTypeAsync(TypeRequest request)
        {
            var caseType = await _context.CaseTypes.FindAsync(request.id);
            if (caseType == null) return;

            caseType.TypeName = request.name;
            _context.CaseTypes.Update(caseType);
            await _context.SaveChangesAsync();
        }
    }
}
