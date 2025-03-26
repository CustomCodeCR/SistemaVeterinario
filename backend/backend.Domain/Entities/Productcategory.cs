using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Productcategory : BaseEntity
{
    public string Categoryname { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Productcategoryrelation> Productcategoryrelations { get; set; } = new List<Productcategoryrelation>();
}
