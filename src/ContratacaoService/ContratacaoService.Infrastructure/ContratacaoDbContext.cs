using Microsoft.EntityFrameworkCore;
using ContratacaoService.Domain;

namespace ContratacaoService.Infrastructure;

public class ContratacaoDbContext : DbContext
{
    public ContratacaoDbContext(DbContextOptions<ContratacaoDbContext> options) : base(options)
    {
    }

    public DbSet<Contratacao> Contratacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contratacao>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PropostaId).IsRequired();
            entity.Property(e => e.DataContratacao).IsRequired();
            entity.Property(e => e.Observacoes).HasMaxLength(1000);
            entity.Property(e => e.DataCriacao).IsRequired();
            
            // Índice único para PropostaId para evitar múltiplas contratações da mesma proposta
            entity.HasIndex(e => e.PropostaId).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}
