using ContratacaoService.Domain;

namespace ContratacaoService.Application.Interfaces;

public interface IContratacaoRepository
{
    Task<Contratacao?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Contratacao>> ObterTodasAsync();
    Task<Contratacao?> ObterPorPropostaIdAsync(Guid propostaId);
    Task<Contratacao> CriarAsync(Contratacao contratacao);
}
