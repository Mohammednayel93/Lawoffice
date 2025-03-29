using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawoffice.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "User name is required")]
        public string username { get; set; }
        [Required(ErrorMessage = "password is required")]
        public string password { get; set; }
        public string? message { get; set; }
    }
}
