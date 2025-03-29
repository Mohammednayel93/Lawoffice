using Lawoffice.DTOs;
using Lawoffice.Models.LawOfficeModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lawoffice.Services.SessionService
{
    public interface ISessionService
    {
        Task<List<Session>> GetSessionsByCaseIdAsync(int caseId);
        Task<Session?> GetByIdAsync(int id);
        Task AddSessionAsync(SessionRequest request);
        Task UpdateSessionAsync(SessionRequest request);
        Task DeleteSessionAsync(int id);

    }
}
