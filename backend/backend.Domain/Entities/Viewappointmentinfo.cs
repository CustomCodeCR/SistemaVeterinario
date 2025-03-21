using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Viewappointmentinfo
{
    public int Appointmentid { get; set; }

    public DateTime Appointmentdate { get; set; }

    public string Reason { get; set; } = null!;

    public string Petname { get; set; } = null!;

    public string Medicspecialty { get; set; } = null!;

    public string Medicphone { get; set; } = null!;
}
