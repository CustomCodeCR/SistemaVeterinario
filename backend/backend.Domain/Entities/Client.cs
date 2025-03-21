using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Client : BaseEntity
{
    public int Userid { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual Appuser User { get; set; } = null!;
}
