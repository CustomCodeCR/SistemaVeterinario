using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Supplier : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Purchaseorder> Purchaseorders { get; set; } = new List<Purchaseorder>();
}
