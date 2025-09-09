namespace PropostaService.Application.DTOs;

public class CriarPropostaDto
{
    public string NomeCliente { get; set; } = string.Empty;
    public string CpfCliente { get; set; } = string.Empty;
    public string EmailCliente { get; set; } = string.Empty;
    public decimal ValorSegurado { get; set; }
    public decimal ValorPremio { get; set; }
    public string? Observacoes { get; set; }
}
