using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Appointment
{
    public int Appointmentid { get; set; }

    public DateTime Appointmentdate { get; set; }

    public string Reason { get; set; } = null!;

    public int Petid { get; set; }

    public int Medicid { get; set; }

    public int? State { get; set; }

    public int Auditcreateuser { get; set; }

    public DateTime Auditcreatedate { get; set; }

    public int? Auditupdateuser { get; set; }

    public DateTime? Auditupdatedate { get; set; }

    public int? Auditdeleteuser { get; set; }

    public DateTime? Auditdeletedate { get; set; }

    public virtual ICollection<Appointmentdetail> Appointmentdetails { get; set; } = new List<Appointmentdetail>();

    public virtual Medic Medic { get; set; } = null!;

    public virtual Pet Pet { get; set; } = null!;
}
