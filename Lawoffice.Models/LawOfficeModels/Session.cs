using System;
using System.Collections.Generic;

namespace Lawoffice.Models.LawOfficeModels;

public partial class Session
{
    public int Id { get; set; }

    public DateTime? SessionDate { get; set; }

    public string? Description { get; set; }

    public string? Descision { get; set; }

    public int? CaseId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Case? Case { get; set; }
}
