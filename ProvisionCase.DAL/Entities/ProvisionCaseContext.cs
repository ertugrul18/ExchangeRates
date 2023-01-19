using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProvisionCase.DAL.Entities;

public partial class ProvisionCaseContext : DbContext
{
    public ProvisionCaseContext()
    {
    }

    public ProvisionCaseContext(DbContextOptions<ProvisionCaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExchangeRate> ExchangeRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=ProvisionCase;User Id=postgres;Password=123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
