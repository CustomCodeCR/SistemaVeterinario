using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Inventory : BaseEntity
{
    public int Productid { get; set; }

    public int Quantity { get; set; }

    public DateTime Updatedate { get; set; }

    public virtual Product Product { get; set; } = null!;
}
