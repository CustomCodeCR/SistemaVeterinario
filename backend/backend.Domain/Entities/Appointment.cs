using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Appointment : BaseEntity
{
    public DateTime Appointmentdate { get; set; }

    public string Reason { get; set; } = null!;

    public int Petid { get; set; }

    public int Medicid { get; set; }

    public virtual ICollection<Appointmentdetail> Appointmentdetails { get; set; } = new List<Appointmentdetail>();

    public virtual Medic Medic { get; set; } = null!;

    public virtual Pet Pet { get; set; } = null!;
}
