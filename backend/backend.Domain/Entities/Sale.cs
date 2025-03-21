using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Sale
{
    public int Saleid { get; set; }

    public DateTime Saledate { get; set; }

    public int Clientid { get; set; }

    public int? State { get; set; }

    public int Auditcreateuser { get; set; }

    public DateTime Auditcreatedate { get; set; }

    public int? Auditupdateuser { get; set; }

    public DateTime? Auditupdatedate { get; set; }

    public int? Auditdeleteuser { get; set; }

    public DateTime? Auditdeletedate { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Saledetail> Saledetails { get; set; } = new List<Saledetail>();
}
