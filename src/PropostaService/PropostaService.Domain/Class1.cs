namespace PropostaService.Domain;

public enum StatusProposta
{
    EmAnalise = 1,
    Aprovada = 2,
    Rejeitada = 3
}

public class Proposta
{
    public Guid Id { get; private set; }
    public string NomeCliente { get; private set; }
    public string CpfCliente { get; private set; }
    public string EmailCliente { get; private set; }
    public decimal ValorSegurado { get; private set; }
    public decimal ValorPremio { get; private set; }
    public string? Observacoes { get; private set; }
    public StatusProposta Status { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataAtualizacao { get; private set; }

    private Proposta() 
    { 
        NomeCliente = string.Empty;
        CpfCliente = string.Empty;
        EmailCliente = string.Empty;
    }

    public Proposta(string nomeCliente, string cpfCliente, string emailCliente, 
                   decimal valorSegurado, decimal valorPremio, string? observacoes = null)
    {
        Id = Guid.NewGuid();
        NomeCliente = nomeCliente;
        CpfCliente = cpfCliente;
        EmailCliente = emailCliente;
        ValorSegurado = valorSegurado;
        ValorPremio = valorPremio;
        Observacoes = observacoes;
        Status = StatusProposta.EmAnalise;
        DataCriacao = DateTime.UtcNow;
    }

    public void AlterarStatus(StatusProposta novoStatus, string? observacoes = null)
    {
        if (Status == novoStatus)
            return;

        Status = novoStatus;
        Observacoes = observacoes ?? Observacoes;
        DataAtualizacao = DateTime.UtcNow;
    }

    public bool PodeSerContratada()
    {
        return Status == StatusProposta.Aprovada;
    }
}
