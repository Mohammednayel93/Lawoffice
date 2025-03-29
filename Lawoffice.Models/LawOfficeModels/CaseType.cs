using System;
using System.Collections.Generic;

namespace Lawoffice.Models.LawOfficeModels;

public partial class CaseType
{
    public int Id { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = new List<Case>();
}
