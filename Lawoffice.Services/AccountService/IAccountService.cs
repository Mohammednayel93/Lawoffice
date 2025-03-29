using Lawoffice.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lawoffice.Services.AccountService
{
   public interface IAccountService
    {
        IEnumerable<Claim> GetUserClaims(LoginRequest customer);

    }
}
