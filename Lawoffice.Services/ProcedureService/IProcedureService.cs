using Lawoffice.DTOs;
using Lawoffice.Models.LawOfficeModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lawoffice.Services.ProcedureService
{
    public interface IProcedureService
    {
        Task<List<Procedure>> GetProceduresByCaseIdAsync(int caseId);
        Task<Procedure?> GetByIdAsync(int id);
        Task AddProcedureAsync(ProcedureRequest request);
        Task UpdateProcedureAsync(ProcedureRequest request);
        Task DeleteProcedureAsync(int id);
    }
}
