using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Appuser : BaseEntity
{
    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Usertype { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Medic> Medics { get; set; } = new List<Medic>();
}
