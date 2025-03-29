using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawoffice.DTOs
{
  public  class SessionRequest
    {
        public int? id { get; set; }
        public int case_id { get; set; }
        public DateTime? session_date { get; set; }
        public string? description { get; set; }
        public string? descision { get; set; }
    }
}
