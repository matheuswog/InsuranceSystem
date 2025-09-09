using ContratacaoService.Application.DTOs;
using ContratacaoService.Application.Interfaces;

namespace ContratacaoService.Application.Queries;

public class ObterContratacaoQuery
{
    public Guid Id { get; set; }
}

public class ObterContratacaoQueryHandler
{
    private readonly IContratacaoRepository _contratacaoRepository;

    public ObterContratacaoQueryHandler(IContratacaoRepository contratacaoRepository)
    {
        _contratacaoRepository = contratacaoRepository;
    }

    public async Task<ContratacaoDto?> HandleAsync(ObterContratacaoQuery query)
    {
        var contratacao = await _contratacaoRepository.ObterPorIdAsync(query.Id);
        if (contratacao == null)
            return null;

        return new ContratacaoDto
        {
            Id = contratacao.Id,
            PropostaId = contratacao.PropostaId,
            DataContratacao = contratacao.DataContratacao,
            Observacoes = contratacao.Observacoes,
            DataCriacao = contratacao.DataCriacao
        };
    }
}
