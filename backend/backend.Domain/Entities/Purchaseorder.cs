using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Purchaseorder
{
    public int Purchaseorderid { get; set; }

    public int Supplierid { get; set; }

    public DateTime Orderdate { get; set; }

    public string Status { get; set; } = null!;

    public int? State { get; set; }

    public int Auditcreateuser { get; set; }

    public DateTime Auditcreatedate { get; set; }

    public int? Auditupdateuser { get; set; }

    public DateTime? Auditupdatedate { get; set; }

    public int? Auditdeleteuser { get; set; }

    public DateTime? Auditdeletedate { get; set; }

    public virtual ICollection<Purchaseorderdetail> Purchaseorderdetails { get; set; } = new List<Purchaseorderdetail>();

    public virtual Supplier Supplier { get; set; } = null!;
}
