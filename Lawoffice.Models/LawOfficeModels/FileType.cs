using System;
using System.Collections.Generic;

namespace Lawoffice.Models.LawOfficeModels;

public partial class FileType
{
    public int Id { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<File> Files { get; set; } = new List<File>();
}
