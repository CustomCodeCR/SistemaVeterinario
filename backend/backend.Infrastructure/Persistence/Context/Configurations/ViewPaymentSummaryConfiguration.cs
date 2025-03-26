// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ViewPaymentSummaryConfiguration : IEntityTypeConfiguration<Viewpaymentsummary>
{
    public void Configure(EntityTypeBuilder<Viewpaymentsummary> builder)
    {
        builder.HasNoKey()
            .ToView("VIEWPAYMENTSUMMARY");

        builder.Property(e => e.Amount)
            .HasColumnType("NUMBER(10,2)")
            .HasColumnName("AMOUNT");
        builder.Property(e => e.Paymentdate)
            .HasColumnType("DATE")
            .HasColumnName("PAYMENTDATE");
        builder.Property(e => e.Paymentid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("PAYMENTID");
        builder.Property(e => e.Paymenttype)
            .HasMaxLength(50)
            .HasColumnName("PAYMENTTYPE");
        builder.Property(e => e.Saleid)
            .HasColumnType("NUMBER")
            .HasColumnName("SALEID");
    }
}