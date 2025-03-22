using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Viewactiveappuser : BaseEntity
{
    public int Userid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Usertype { get; set; } = null!;
}
