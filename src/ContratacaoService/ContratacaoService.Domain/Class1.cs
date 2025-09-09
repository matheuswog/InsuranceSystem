namespace ContratacaoService.Domain;

public class Contratacao
{
    public Guid Id { get; private set; }
    public Guid PropostaId { get; private set; }
    public DateTime DataContratacao { get; private set; }
    public string? Observacoes { get; private set; }
    public DateTime DataCriacao { get; private set; }

    private Contratacao() { } // Para EF Core

    public Contratacao(Guid propostaId, string? observacoes = null)
    {
        Id = Guid.NewGuid();
        PropostaId = propostaId;
        DataContratacao = DateTime.UtcNow;
        Observacoes = observacoes;
        DataCriacao = DateTime.UtcNow;
    }
}
