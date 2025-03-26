using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Pet : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Breed { get; set; } = null!;

    public int Age { get; set; }

    public int Clientid { get; set; }

    public virtual ICollection<Appliedvaccine> Appliedvaccines { get; set; } = new List<Appliedvaccine>();

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Client Client { get; set; } = null!;
}
