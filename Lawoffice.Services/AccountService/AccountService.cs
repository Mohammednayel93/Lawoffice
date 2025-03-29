using Lawoffice.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lawoffice.Services.AccountService
{
   public class AccountService:IAccountService
    {
        public IEnumerable<Claim> GetUserClaims(LoginRequest customer)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, customer.username),
                 new Claim(ClaimTypes.Role, "admin")
            };
        }
    }
}
