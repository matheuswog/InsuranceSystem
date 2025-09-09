using PropostaService.Application.DTOs;
using PropostaService.Application.Interfaces;
using PropostaService.Domain;

namespace PropostaService.Application.Queries;

public class ListarPropostasQuery
{
    public StatusProposta? Status { get; set; }
}

public class ListarPropostasQueryHandler
{
    private readonly IPropostaRepository _propostaRepository;

    public ListarPropostasQueryHandler(IPropostaRepository propostaRepository)
    {
        _propostaRepository = propostaRepository;
    }

    public async Task<IEnumerable<PropostaDto>> HandleAsync(ListarPropostasQuery query)
    {
        IEnumerable<Proposta> propostas;

        if (query.Status.HasValue)
        {
            propostas = await _propostaRepository.ObterPorStatusAsync(query.Status.Value);
        }
        else
        {
            propostas = await _propostaRepository.ObterTodasAsync();
        }

        return propostas.Select(p => new PropostaDto
        {
            Id = p.Id,
            NomeCliente = p.NomeCliente,
            CpfCliente = p.CpfCliente,
            EmailCliente = p.EmailCliente,
            ValorSegurado = p.ValorSegurado,
            ValorPremio = p.ValorPremio,
            Observacoes = p.Observacoes,
            Status = p.Status,
            DataCriacao = p.DataCriacao,
            DataAtualizacao = p.DataAtualizacao
        });
    }
}
