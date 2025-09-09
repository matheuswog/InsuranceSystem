namespace ContratacaoService.Application.Interfaces;

public interface IPropostaServiceClient
{
    Task<bool> VerificarPropostaExisteAsync(Guid propostaId);
    Task<bool> VerificarPropostaAprovadaAsync(Guid propostaId);
}
