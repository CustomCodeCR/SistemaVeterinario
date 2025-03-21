using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Productcategory
{
    public int Categoryid { get; set; }

    public string Categoryname { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? State { get; set; }

    public int Auditcreateuser { get; set; }

    public DateTime Auditcreatedate { get; set; }

    public int? Auditupdateuser { get; set; }

    public DateTime? Auditupdatedate { get; set; }

    public int? Auditdeleteuser { get; set; }

    public DateTime? Auditdeletedate { get; set; }

    public virtual ICollection<Productcategoryrelation> Productcategoryrelations { get; set; } = new List<Productcategoryrelation>();
}
