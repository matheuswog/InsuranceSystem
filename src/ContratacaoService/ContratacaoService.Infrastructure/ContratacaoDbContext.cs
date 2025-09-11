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
            
            entity.HasIndex(e => e.PropostaId).IsUnique();
            
            // TODO: Considerar adicionar índice composto para consultas por data
        });

        base.OnModelCreating(modelBuilder);
    }
}
