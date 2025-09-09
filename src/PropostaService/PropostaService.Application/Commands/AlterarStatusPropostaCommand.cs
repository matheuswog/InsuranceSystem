using PropostaService.Application.DTOs;
using PropostaService.Application.Interfaces;
using PropostaService.Domain;

namespace PropostaService.Application.Commands;

public class AlterarStatusPropostaCommand
{
    public Guid Id { get; set; }
    public AlterarStatusPropostaDto Dto { get; set; } = new();
}

public class AlterarStatusPropostaCommandHandler
{
    private readonly IPropostaRepository _propostaRepository;

    public AlterarStatusPropostaCommandHandler(IPropostaRepository propostaRepository)
    {
        _propostaRepository = propostaRepository;
    }

    public async Task<PropostaDto?> HandleAsync(AlterarStatusPropostaCommand command)
    {
        var proposta = await _propostaRepository.ObterPorIdAsync(command.Id);
        if (proposta == null)
            return null;

        proposta.AlterarStatus(command.Dto.Status, command.Dto.Observacoes);
        await _propostaRepository.AtualizarAsync(proposta);

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
