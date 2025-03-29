using System;
using System.Collections.Generic;

namespace Lawoffice.Models.LawOfficeModels;

public partial class File
{
    public int Id { get; set; }

    public int? CaseId { get; set; }

    public int? FileTypeId { get; set; }

    public string? FileUrl { get; set; }

    public int? Type { get; set; }

    public DateTime? CretaedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Case? Case { get; set; }

    public virtual FileType? FileType { get; set; }
}
