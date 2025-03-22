using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Saledetail : BaseEntity
{
    public int Saleid { get; set; }

    public int Productid { get; set; }

    public int Quantity { get; set; }

    public int Price { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Sale Sale { get; set; } = null!;
}
