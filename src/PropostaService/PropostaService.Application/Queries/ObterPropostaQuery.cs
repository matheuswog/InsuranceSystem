using PropostaService.Application.DTOs;
using PropostaService.Application.Interfaces;
using PropostaService.Domain;

namespace PropostaService.Application.Queries;

public class ObterPropostaQuery
{
    public Guid Id { get; set; }
}

public class ObterPropostaQueryHandler
{
    private readonly IPropostaRepository _propostaRepository;

    public ObterPropostaQueryHandler(IPropostaRepository propostaRepository)
    {
        _propostaRepository = propostaRepository;
    }

    public async Task<PropostaDto?> HandleAsync(ObterPropostaQuery query)
    {
        var proposta = await _propostaRepository.ObterPorIdAsync(query.Id);
        if (proposta == null)
            return null;

        return new PropostaDto
        {
            Id = proposta.Id,
            NomeCliente = proposta.NomeCliente,
            CpfCliente = proposta.CpfCliente,
            EmailCliente = proposta.EmailCliente,
            ValorSegurado = proposta.ValorSegurado,
            ValorPremio = proposta.ValorPremio,
            Observacoes = proposta.Observacoes,
            Status = proposta.Status,
            DataCriacao = proposta.DataCriacao,
            DataAtualizacao = proposta.DataAtualizacao
        };
    }
}
