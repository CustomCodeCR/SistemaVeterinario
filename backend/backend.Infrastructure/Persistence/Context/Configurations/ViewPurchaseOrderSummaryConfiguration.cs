// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ViewPurchaseOrderSummaryConfiguration : IEntityTypeConfiguration<Viewpurchaseordersummary>
{
    public void Configure(EntityTypeBuilder<Viewpurchaseordersummary> builder)
    {
        builder
                .HasNoKey()
                .ToView("VIEWPURCHASEORDERSUMMARY");

        builder.Property(e => e.Orderdate)
            .HasColumnType("DATE")
            .HasColumnName("ORDERDATE");
        builder.Property(e => e.Purchaseorderid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("PURCHASEORDERID");
        builder.Property(e => e.Status)
            .HasMaxLength(50)
            .HasColumnName("STATUS");
        builder.Property(e => e.Supplierid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("SUPPLIERID");
        builder.Property(e => e.Suppliername)
            .HasMaxLength(50)
            .HasColumnName("SUPPLIERNAME");
    }
}