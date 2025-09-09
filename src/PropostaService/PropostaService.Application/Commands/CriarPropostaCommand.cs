using PropostaService.Application.DTOs;
using PropostaService.Application.Interfaces;
using PropostaService.Domain;

namespace PropostaService.Application.Commands;

public class CriarPropostaCommand
{
    public CriarPropostaDto Dto { get; set; } = new();
}

public class CriarPropostaCommandHandler
{
    private readonly IPropostaRepository _propostaRepository;

    public CriarPropostaCommandHandler(IPropostaRepository propostaRepository)
    {
        _propostaRepository = propostaRepository;
    }

    public async Task<PropostaDto> HandleAsync(CriarPropostaCommand command)
    {
        var proposta = new Proposta(
            command.Dto.NomeCliente,
            command.Dto.CpfCliente,
            command.Dto.EmailCliente,
            command.Dto.ValorSegurado,
            command.Dto.ValorPremio,
            command.Dto.Observacoes
        );

        var propostaCriada = await _propostaRepository.CriarAsync(proposta);

        return new PropostaDto
        {
            Id = propostaCriada.Id,
            NomeCliente = propostaCriada.NomeCliente,
            CpfCliente = propostaCriada.CpfCliente,
            EmailCliente = propostaCriada.EmailCliente,
            ValorSegurado = propostaCriada.ValorSegurado,
            ValorPremio = propostaCriada.ValorPremio,
            Observacoes = propostaCriada.Observacoes,
            Status = propostaCriada.Status,
            DataCriacao = propostaCriada.DataCriacao,
            DataAtualizacao = propostaCriada.DataAtualizacao
        };
    }
}
