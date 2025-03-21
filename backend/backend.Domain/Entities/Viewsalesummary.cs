using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Viewsalesummary
{
    public int Saleid { get; set; }

    public DateTime Saledate { get; set; }

    public int Clientid { get; set; }

    public string Address { get; set; } = null!;

    public int? Totalsale { get; set; }
}
