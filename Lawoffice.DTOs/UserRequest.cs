using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawoffice.DTOs
{
 public   class UserRequest
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? identity_number { get; set; }
        public string? phone_number_1 { get; set; }
        public string? phone_number_2 { get; set; }
        public string? message { get; set; }

    }
}
