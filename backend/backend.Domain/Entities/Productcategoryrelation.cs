using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Productcategoryrelation : BaseEntity
{
    public int Productid { get; set; }

    public int Categoryid { get; set; }

    public virtual Productcategory Category { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
