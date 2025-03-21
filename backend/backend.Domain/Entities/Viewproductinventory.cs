using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Viewproductinventory
{
    public int Productid { get; set; }

    public string Productname { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public int Quantity { get; set; }

    public DateTime Updatedate { get; set; }
}
