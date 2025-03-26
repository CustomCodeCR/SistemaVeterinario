using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Appointmentdetail : BaseEntity
{
    public int Appointmentid { get; set; }

    public string Diagnosis { get; set; } = null!;

    public string Treatment { get; set; } = null!;

    public string Observations { get; set; } = null!;

    public virtual Appointment Appointment { get; set; } = null!;
}
