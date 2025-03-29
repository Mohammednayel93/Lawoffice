using Lawoffice.DTOs;
using Lawoffice.Models.LawOfficeModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawoffice.Services.SessionService
{
    public class SessionService : ISessionService
    {
        private readonly LawOfficeContext _context;

        public SessionService(LawOfficeContext context)
        {
            _context = context;
        }

        // ✅ Get all sessions by case ID
        public async Task<List<Session>> GetSessionsByCaseIdAsync(int caseId)
        {
            return await _context.Sessions
                .Where(s => s.CaseId == caseId)
                .OrderByDescending(s => s.SessionDate)
                .ToListAsync();
        }

        // ✅ Get single session by ID
        public async Task<Session?> GetByIdAsync(int id)
        {
            return await _context.Sessions.FindAsync(id);
        }

        // ✅ Add session
        public async Task AddSessionAsync(SessionRequest request)
        {
            var session = new Session
            {
                CaseId = request.case_id,
                SessionDate = request.session_date,
                Description = request.description,
                Descision = request.descision,
                CreatedAt = DateTime.UtcNow
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
        }

        // ✅ Update session
        public async Task UpdateSessionAsync(SessionRequest request)
        {
            var existing = await _context.Sessions.FindAsync(request.id);
            if (existing == null) return;

            existing.SessionDate = request.session_date;
            existing.Description = request.description;
            existing.Descision = request.descision;
            existing.UpdatedAt = DateTime.UtcNow;

            _context.Sessions.Update(existing);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteSessionAsync(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null) return;

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
        }

    }
}
