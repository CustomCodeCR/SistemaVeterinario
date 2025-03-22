using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Purchaseorderdetail : BaseEntity
{
    public int Purchaseorderid { get; set; }
    public int Productid { get; set; }

    public int Quantity { get; set; }

    public int Unitprice { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Purchaseorder Purchaseorder { get; set; } = null!;
}
