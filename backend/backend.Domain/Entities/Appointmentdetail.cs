using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Appointmentdetail
{
    public int Appointmentdetailid { get; set; }

    public int Appointmentid { get; set; }

    public string Diagnosis { get; set; } = null!;

    public string Treatment { get; set; } = null!;

    public string Observations { get; set; } = null!;

    public int? State { get; set; }

    public int Auditcreateuser { get; set; }

    public DateTime Auditcreatedate { get; set; }

    public int? Auditupdateuser { get; set; }

    public DateTime? Auditupdatedate { get; set; }

    public int? Auditdeleteuser { get; set; }

    public DateTime? Auditdeletedate { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
}
