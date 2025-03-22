using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Sale : BaseEntity
{
    public DateTime Saledate { get; set; }

    public int Clientid { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Saledetail> Saledetails { get; set; } = new List<Saledetail>();
}
