using System;
using System.Collections.Generic;

namespace Lawoffice.Models.LawOfficeModels;

public partial class Case
{
    public int Id { get; set; }

    public int? ClientId { get; set; }

    public int? OpponentId { get; set; }

    public int? CaseTypeId { get; set; }

    public string? Description { get; set; }

    public DateTime? FilingLawsuitDate { get; set; }

    public string? CourtName { get; set; }

    public string? LawsuitNumber { get; set; }

    public string? PowerOfAttorneyNumber { get; set; }

    public decimal? Fees { get; set; }

    public decimal? PaymentOfFees { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual CaseType? CaseType { get; set; }

    public virtual User? Client { get; set; }

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual User? Opponent { get; set; }

    public virtual ICollection<Procedure> Procedures { get; set; } = new List<Procedure>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
