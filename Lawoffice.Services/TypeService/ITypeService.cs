using Lawoffice.DTOs;
using Lawoffice.Models.LawOfficeModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lawoffice.Services.TypeService
{
    public interface ITypeService
    {
        // FileType
        Task<List<FileType>> GetAllFileTypesAsync();
        Task<FileType?> GetFileTypeByIdAsync(int id);
        Task AddFileTypeAsync(TypeRequest request);
        Task UpdateFileTypeAsync(TypeRequest request);

        // CaseType
        Task<List<CaseType>> GetAllCaseTypesAsync();
        Task<CaseType?> GetCaseTypeByIdAsync(int id);
        Task AddCaseTypeAsync(TypeRequest request);
        Task UpdateCaseTypeAsync(TypeRequest request);
    }
}
