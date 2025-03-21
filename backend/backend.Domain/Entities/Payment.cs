using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Payment
{
    public int Paymentid { get; set; }

    public int Saleid { get; set; }

    public int Amount { get; set; }

    public DateTime Paymentdate { get; set; }

    public string Paymenttype { get; set; } = null!;

    public int? State { get; set; }

    public int Auditcreateuser { get; set; }

    public DateTime Auditcreatedate { get; set; }

    public int? Auditupdateuser { get; set; }

    public DateTime? Auditupdatedate { get; set; }

    public int? Auditdeleteuser { get; set; }

    public DateTime? Auditdeletedate { get; set; }

    public virtual Sale Sale { get; set; } = null!;
}
