// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Context.Configurations;

public class ViewSaleSummaryConfiguration : IEntityTypeConfiguration<Viewsalesummary>
{
    public void Configure(EntityTypeBuilder<Viewsalesummary> builder)
    {
        builder
                .HasNoKey()
                .ToView("VIEWSALESUMMARY");

        builder.Property(e => e.Address)
            .HasMaxLength(100)
            .HasColumnName("ADDRESS");
        builder.Property(e => e.Clientid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("CLIENTID");
        builder.Property(e => e.Saledate)
            .HasColumnType("DATE")
            .HasColumnName("SALEDATE");
        builder.Property(e => e.Saleid)
            .ValueGeneratedOnAdd()
            .HasColumnType("NUMBER")
            .HasColumnName("SALEID");
        builder.Property(e => e.Totalsale)
            .HasColumnType("NUMBER")
            .HasColumnName("TOTALSALE");
    }
}