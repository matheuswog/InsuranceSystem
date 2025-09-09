using PropostaService.Domain;

namespace PropostaService.Application.DTOs;

public class PropostaDto
{
    public Guid Id { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string CpfCliente { get; set; } = string.Empty;
    public string EmailCliente { get; set; } = string.Empty;
    public decimal ValorSegurado { get; set; }
    public decimal ValorPremio { get; set; }
    public string? Observacoes { get; set; }
    public StatusProposta Status { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
}
