using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Viewclientinfo
{
    public int Clientid { get; set; }

    public int Userid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime Auditcreatedate { get; set; }
}
