using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Viewproductcategorymapping : BaseEntity
{
    public int Productid { get; set; }

    public string Productname { get; set; } = null!;

    public int Categoryid { get; set; }

    public string Categoryname { get; set; } = null!;
}
