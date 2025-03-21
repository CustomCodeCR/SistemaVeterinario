using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Purchaseorderdetail
{
    public int Purchaseorderid { get; set; }

    public int Productid { get; set; }

    public int Quantity { get; set; }

    public int Unitprice { get; set; }

    public int? State { get; set; }

    public int Auditcreateuser { get; set; }

    public DateTime Auditcreatedate { get; set; }

    public int? Auditupdateuser { get; set; }

    public DateTime? Auditupdatedate { get; set; }

    public int? Auditdeleteuser { get; set; }

    public DateTime? Auditdeletedate { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Purchaseorder Purchaseorder { get; set; } = null!;
}
