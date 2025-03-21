using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Viewpurchaseordersummary
{
    public int Purchaseorderid { get; set; }

    public DateTime Orderdate { get; set; }

    public string Status { get; set; } = null!;

    public int Supplierid { get; set; }

    public string Suppliername { get; set; } = null!;
}
