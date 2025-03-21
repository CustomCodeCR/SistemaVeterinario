using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Productcategoryrelation
{
    public int Productid { get; set; }

    public int Categoryid { get; set; }

    public int? State { get; set; }

    public int Auditcreateuser { get; set; }

    public DateTime Auditcreatedate { get; set; }

    public int? Auditupdateuser { get; set; }

    public DateTime? Auditupdatedate { get; set; }

    public int? Auditdeleteuser { get; set; }

    public DateTime? Auditdeletedate { get; set; }

    public virtual Productcategory Category { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
