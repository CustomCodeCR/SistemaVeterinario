using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Viewpetdetail : BaseEntity
{
    public int Petid { get; set; }

    public string Petname { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Breed { get; set; } = null!;

    public int Age { get; set; }

    public int Clientid { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;
}
