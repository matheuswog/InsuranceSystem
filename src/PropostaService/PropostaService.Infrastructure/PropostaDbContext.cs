using Microsoft.EntityFrameworkCore;
using PropostaService.Domain;

namespace PropostaService.Infrastructure;

public class PropostaDbContext : DbContext
{
    public PropostaDbContext(DbContextOptions<PropostaDbContext> options) : base(options)
    {
    }

    public DbSet<Proposta> Propostas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Proposta>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NomeCliente).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CpfCliente).IsRequired().HasMaxLength(11);
            entity.Property(e => e.EmailCliente).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ValorSegurado).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ValorPremio).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Observacoes).HasMaxLength(1000);
            entity.Property(e => e.Status).HasConversion<int>();
            entity.Property(e => e.DataCriacao).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
