using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Medic : BaseEntity
{
    public int Userid { get; set; }

    public string Specialty { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Appuser User { get; set; } = null!;
}
