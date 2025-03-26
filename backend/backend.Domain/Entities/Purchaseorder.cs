using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Purchaseorder : BaseEntity
{
    public int Supplierid { get; set; }

    public DateTime Orderdate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Purchaseorderdetail> Purchaseorderdetails { get; set; } = new List<Purchaseorderdetail>();

    public virtual Supplier Supplier { get; set; } = null!;
}
