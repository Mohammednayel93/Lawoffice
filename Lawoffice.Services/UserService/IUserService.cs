using Lawoffice.DTOs;
using Lawoffice.Models;
using Lawoffice.Models.LawOfficeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Lawoffice.Services.UserService
{
   public interface IUserService
    {
        Task<IPagedList<User>> GetAllUsersAsync(int page = 1, int pageSize = 20, string keyword = "");
        Task<User> GetUserByIdAsync(int userId);
        Task<List<User>> GetAllUsersListAsync();
        Task AddUserAsync(UserRequest request);
        Task UpdateUserAsync(UserRequest request);
        Task<bool> IsPhoneExistsAsync(string phone, int? userId = null);
    }
}
