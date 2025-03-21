// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => e.Paymentid).HasName("SYS_C008125");

        builder.ToTable("PAYMENT");

        builder.HasIndex(e => e.Saleid, "IDX_PAYMENT_SALEID");

        builder.Property(e => e.Paymentid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("PAYMENTID");
        builder.Property(e => e.Amount)
            .HasColumnType("NUMBER(10,2)")
            .HasColumnName("AMOUNT");
        builder.Property(e => e.Auditcreatedate)
            .HasPrecision(7)
            .HasColumnName("AUDITCREATEDATE");
        builder.Property(e => e.Auditcreateuser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITCREATEUSER");
        builder.Property(e => e.Auditdeletedate)
            .HasPrecision(7)
            .HasColumnName("AUDITDELETEDATE");
        builder.Property(e => e.Auditdeleteuser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITDELETEUSER");
        builder.Property(e => e.Auditupdatedate)
            .HasPrecision(7)
            .HasColumnName("AUDITUPDATEDATE");
        builder.Property(e => e.Auditupdateuser)
            .HasColumnType("NUMBER")
            .HasColumnName("AUDITUPDATEUSER");
        builder.Property(e => e.Paymentdate)
            .HasColumnType("DATE")
            .HasColumnName("PAYMENTDATE");
        builder.Property(e => e.Paymenttype)
            .HasMaxLength(50)
            .HasColumnName("PAYMENTTYPE");
        builder.Property(e => e.Saleid)
            .HasColumnType("NUMBER")
            .HasColumnName("SALEID");
        builder.Property(e => e.State)
            .HasColumnType("NUMBER")
            .HasColumnName("STATE");

        builder.HasOne(d => d.Sale).WithMany(p => p.Payments)
            .HasForeignKey(d => d.Saleid)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_PAYMENT_SALE");
    }
}