using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Appliedvaccine
{
    public int Appliedvaccineid { get; set; }

    public DateTime Applicationdate { get; set; }

    public int Petid { get; set; }

    public int Vaccineid { get; set; }

    public int? State { get; set; }

    public int Auditcreateuser { get; set; }

    public DateTime Auditcreatedate { get; set; }

    public int? Auditupdateuser { get; set; }

    public DateTime? Auditupdatedate { get; set; }

    public int? Auditdeleteuser { get; set; }

    public DateTime? Auditdeletedate { get; set; }

    public virtual Pet Pet { get; set; } = null!;

    public virtual Vaccine Vaccine { get; set; } = null!;
}
