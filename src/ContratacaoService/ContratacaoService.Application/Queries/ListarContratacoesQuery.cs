using ContratacaoService.Application.DTOs;
using ContratacaoService.Application.Interfaces;
using ContratacaoService.Domain;

namespace ContratacaoService.Application.Queries;

public class ListarContratacoesQuery
{
    public Guid? PropostaId { get; set; }
}

public class ListarContratacoesQueryHandler
{
    private readonly IContratacaoRepository _contratacaoRepository;

    public ListarContratacoesQueryHandler(IContratacaoRepository contratacaoRepository)
    {
        _contratacaoRepository = contratacaoRepository;
    }

    public async Task<IEnumerable<ContratacaoDto>> HandleAsync(ListarContratacoesQuery query)
    {
        IEnumerable<Contratacao> contratacoes;

        if (query.PropostaId.HasValue)
        {
            var contratacao = await _contratacaoRepository.ObterPorPropostaIdAsync(query.PropostaId.Value);
            contratacoes = contratacao != null ? new[] { contratacao } : Array.Empty<Contratacao>();
        }
        else
        {
            contratacoes = await _contratacaoRepository.ObterTodasAsync();
        }

        return contratacoes.Select(c => new ContratacaoDto
        {
            Id = c.Id,
            PropostaId = c.PropostaId,
            DataContratacao = c.DataContratacao,
            Observacoes = c.Observacoes,
            DataCriacao = c.DataCriacao
        });
    }
}
