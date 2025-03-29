using Lawoffice.Models.LawOfficeModels;
using Lawoffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using X.PagedList.EF;
using Lawoffice.DTOs;

namespace Lawoffice.Services.UserService
{
   public class UserService:IUserService
    {
        private readonly LawOfficeContext _context;
        public UserService(LawOfficeContext context)
        {
            _context = context;
        }
        public async Task<IPagedList<User>> GetAllUsersAsync( int page = 1, int pageSize = 20, string keyword = "")
        {
            var query = _context.Users  
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(u => u.Name.Contains(keyword)
                || u.Email.Contains(keyword)
                || u.IdentityNumber.Contains(keyword)
                || u.PhoneNumber1.Contains(keyword));
            }
            

            return await query.ToPagedListAsync(page, pageSize);
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users 
                .FirstOrDefaultAsync(m => m.Id == userId);
        }

        
        public async Task<List<User>> GetAllUsersListAsync()
        {
            return await _context.Users
                               .ToListAsync();
        }
        public async Task AddUserAsync(UserRequest request)
        {
            var user = new User
            {
                Name = request.name,
                Email = request.email,
                PhoneNumber1 = request.phone_number_1,
                PhoneNumber2 = request.phone_number_2,  
                IdentityNumber = request.identity_number,
                 CreatedAt=DateTime.Now,
                 UpdatedAt=DateTime.Now,
                 IsActive=true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(); 
            
        }

        public async Task UpdateUserAsync(UserRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.id);
            if (user == null) return;

            user.Name = request.name;
            user.Email = request.email;
            user.PhoneNumber1 = request.phone_number_1;
            
            user.PhoneNumber2 = request.phone_number_2;
            user.IdentityNumber = request.identity_number;

           

            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsPhoneExistsAsync(string phone, int? userId = null)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(u => u.PhoneNumber1.Trim() == phone.Trim());
            } 
            if (userId.HasValue)
            {
                query = query.Where(u => u.Id != userId); // Exclude current user if editing
            }

            return await query.AnyAsync();
        }
      

     

    }
}
