using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawoffice.DTOs
{
  public  class ProcedureRequest
    {
        public int? id { get; set; }
        public DateTime? procedure_date { get; set; }
        public string? decription { get; set; }
        public int case_id { get; set; }
    }
}
