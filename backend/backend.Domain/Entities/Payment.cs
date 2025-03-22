using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Payment : BaseEntity
{
    public int Saleid { get; set; }

    public int Amount { get; set; }

    public DateTime Paymentdate { get; set; }

    public string Paymenttype { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}
