using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MathApp.Models;

public partial class MathDbContext : DbContext
{
    public MathDbContext()
    {
    }

    public MathDbContext(DbContextOptions<MathDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MathCalculation> MathCalculations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MathCalculation>(entity =>
        {
            entity.HasKey(e => e.CalculationId).HasName("PK__MathCalc__57C05F66C3B1E4E5");

            entity.Property(e => e.CalculationId).HasColumnName("CalculationID");
            entity.Property(e => e.FirstNumber).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Result).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SecondNumber).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
