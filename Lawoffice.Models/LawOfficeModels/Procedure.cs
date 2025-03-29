using System;
using System.Collections.Generic;

namespace Lawoffice.Models.LawOfficeModels;

public partial class Procedure
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public DateTime? ProcedureDate { get; set; }

    public int? CaseId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Case? Case { get; set; }
}
