using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Product
{
    public int Productid { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public int? State { get; set; }

    public int Auditcreateuser { get; set; }

    public DateTime Auditcreatedate { get; set; }

    public int? Auditupdateuser { get; set; }

    public DateTime? Auditupdatedate { get; set; }

    public int? Auditdeleteuser { get; set; }

    public DateTime? Auditdeletedate { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Productcategoryrelation> Productcategoryrelations { get; set; } = new List<Productcategoryrelation>();

    public virtual ICollection<Purchaseorderdetail> Purchaseorderdetails { get; set; } = new List<Purchaseorderdetail>();

    public virtual ICollection<Saledetail> Saledetails { get; set; } = new List<Saledetail>();
}
