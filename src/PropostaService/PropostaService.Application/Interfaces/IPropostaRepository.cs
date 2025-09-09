using PropostaService.Domain;

namespace PropostaService.Application.Interfaces;

public interface IPropostaRepository
{
    Task<Proposta?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Proposta>> ObterTodasAsync();
    Task<IEnumerable<Proposta>> ObterPorStatusAsync(StatusProposta status);
    Task<Proposta> CriarAsync(Proposta proposta);
    Task<Proposta> AtualizarAsync(Proposta proposta);
    Task<bool> ExisteAsync(Guid id);
}
