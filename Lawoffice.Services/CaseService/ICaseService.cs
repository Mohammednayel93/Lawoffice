using Lawoffice.Models;
using Lawoffice.Models.LawOfficeModels;
using X.PagedList;
using Microsoft.AspNetCore.Http;

namespace Lawoffice.Services.CaseService
{
    public interface ICaseService
    {
        Task<IPagedList<Case>> GetCasesAsync(int page = 1, int pageSize = 20,
                                              string? caseNumber = null,
                                             string? courtName = null,
                                             int? clientId = null,
                                             int? opponentId = null);

        Task<Case?> GetCaseByIdAsync(int id);

        Task AddCaseAsync(CaseRequest request);
        Task UpdateCaseAsync(CaseRequest request);

        Task UploadCaseFilesAsync(int caseId, int fileTypeId, List<IFormFile> files);
        Task DeleteCaseFileAsync(int fileId);
        Task<Models.LawOfficeModels.File?> GetCaseFileByIdAsync(int id);

    }
}
