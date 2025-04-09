using System;
using System.Collections.Generic;

namespace backend.Domain.Entities;

public partial class Product : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Image {  get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<Productcategoryrelation> Productcategoryrelations { get; set; } = new List<Productcategoryrelation>();

    public virtual ICollection<Purchaseorderdetail> Purchaseorderdetails { get; set; } = new List<Purchaseorderdetail>();

    public virtual ICollection<Saledetail> Saledetails { get; set; } = new List<Saledetail>();
}
