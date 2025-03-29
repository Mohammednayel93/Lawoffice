using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawoffice.Models
{
   public class CaseRequest
    {
        public int? id { get; set; }
        public int client_id { get; set; }
        public int opponent_id { get; set; }
        public string? description { get; set; }
        public int? case_type_id { get; set; }
        public DateTime? filing_lawsuit_date { get; set; }
        public string? court_name { get; set; }
        public string lawsuit_number { get; set; }
        public string? power_of_attorney_number { get; set; }
        public decimal? fees { get; set; }
        public decimal? fees_payment { get; set; }
        public string? message { get; set; }

    }
}
