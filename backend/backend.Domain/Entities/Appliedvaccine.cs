using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Appliedvaccine : BaseEntity
{
    public DateTime Applicationdate { get; set; }

    public int Petid { get; set; }

    public int Vaccineid { get; set; }

    public virtual Pet Pet { get; set; } = null!;

    public virtual Vaccine Vaccine { get; set; } = null!;
}
