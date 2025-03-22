using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Viewvaccineinfo : BaseEntity
{
    public int Appliedvaccineid { get; set; }

    public DateTime Applicationdate { get; set; }

    public string Petname { get; set; } = null!;

    public string Vaccinename { get; set; } = null!;

    public string Description { get; set; } = null!;
}
