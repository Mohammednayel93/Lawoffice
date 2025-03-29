using System;
using System.Collections.Generic;

namespace Lawoffice.Models.LawOfficeModels;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? IdentityNumber { get; set; }

    public string? PhoneNumber1 { get; set; }

    public string? PhoneNumber2 { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Case> CaseClients { get; set; } = new List<Case>();

    public virtual ICollection<Case> CaseOpponents { get; set; } = new List<Case>();
}
