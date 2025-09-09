using PropostaService.Domain;

namespace PropostaService.Application.DTOs;

public class AlterarStatusPropostaDto
{
    public StatusProposta Status { get; set; }
    public string? Observacoes { get; set; }
}
