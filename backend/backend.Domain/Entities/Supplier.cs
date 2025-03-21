using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Supplier
{
    public int Supplierid { get; set; }

    public string Name { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? State { get; set; }

    public int Auditcreateuser { get; set; }

    public DateTime Auditcreatedate { get; set; }

    public int? Auditupdateuser { get; set; }

    public DateTime? Auditupdatedate { get; set; }

    public int? Auditdeleteuser { get; set; }

    public DateTime? Auditdeletedate { get; set; }

    public virtual ICollection<Purchaseorder> Purchaseorders { get; set; } = new List<Purchaseorder>();
}
