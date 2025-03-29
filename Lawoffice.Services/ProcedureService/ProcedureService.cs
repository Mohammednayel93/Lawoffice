using Lawoffice.DTOs;
using Lawoffice.Models.LawOfficeModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawoffice.Services.ProcedureService
{
    public class ProcedureService : IProcedureService
    {
        private readonly LawOfficeContext _context;

        public ProcedureService(LawOfficeContext context)
        {
            _context = context;
        }

        public async Task<List<Procedure>> GetProceduresByCaseIdAsync(int caseId)
        {
            return await _context.Procedures
                .Where(p => p.CaseId == caseId)
                .OrderByDescending(p => p.ProcedureDate)
                .ToListAsync();
        }

        public async Task<Procedure?> GetByIdAsync(int id)
        {
            return await _context.Procedures.FindAsync(id);
        }

        public async Task AddProcedureAsync(ProcedureRequest request)
        {
            var procedure = new Procedure
            {
                CaseId = request.case_id,
                ProcedureDate = request.procedure_date,
                Description = request.decription,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Procedures.Add(procedure);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProcedureAsync(ProcedureRequest request)
        {
            var existing = await _context.Procedures.FindAsync(request.id);
            if (existing == null) return;

            existing.ProcedureDate = request.procedure_date;
            existing.Description = request.decription;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Procedures.Update(existing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProcedureAsync(int id)
        {
            var procedure = await _context.Procedures.FindAsync(id);
            if (procedure == null) return;

            _context.Procedures.Remove(procedure);
            await _context.SaveChangesAsync();
        }
    }
}
