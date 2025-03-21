using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Vaccine : BaseEntity
{
    public string Vaccinename { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;

    public virtual ICollection<Appliedvaccine> Appliedvaccines { get; set; } = new List<Appliedvaccine>();
}
