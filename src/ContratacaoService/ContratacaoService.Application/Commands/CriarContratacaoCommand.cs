using ContratacaoService.Application.DTOs;
using ContratacaoService.Application.Interfaces;
using ContratacaoService.Domain;

namespace ContratacaoService.Application.Commands;

public class CriarContratacaoCommand
{
    public CriarContratacaoDto Dto { get; set; } = new();
}

public class CriarContratacaoCommandHandler
{
    private readonly IContratacaoRepository _contratacaoRepository;
    private readonly IPropostaServiceClient _propostaServiceClient;

    public CriarContratacaoCommandHandler(
        IContratacaoRepository contratacaoRepository,
        IPropostaServiceClient propostaServiceClient)
    {
        _contratacaoRepository = contratacaoRepository;
        _propostaServiceClient = propostaServiceClient;
    }

    public async Task<ContratacaoDto?> HandleAsync(CriarContratacaoCommand command)
    {
        // Verificar se a proposta existe
        var propostaExiste = await _propostaServiceClient.VerificarPropostaExisteAsync(command.Dto.PropostaId);
        if (!propostaExiste)
            return null;

        // Verificar se a proposta está aprovada
        var propostaAprovada = await _propostaServiceClient.VerificarPropostaAprovadaAsync(command.Dto.PropostaId);
        if (!propostaAprovada)
            return null;

        // Verificar se já existe contratação para esta proposta
        var contratacaoExistente = await _contratacaoRepository.ObterPorPropostaIdAsync(command.Dto.PropostaId);
        if (contratacaoExistente != null)
            return null;

        var contratacao = new Contratacao(command.Dto.PropostaId, command.Dto.Observacoes);
        var contratacaoCriada = await _contratacaoRepository.CriarAsync(contratacao);

        return new ContratacaoDto
        {
            Id = contratacaoCriada.Id,
            PropostaId = contratacaoCriada.PropostaId,
            DataContratacao = contratacaoCriada.DataContratacao,
            Observacoes = contratacaoCriada.Observacoes,
            DataCriacao = contratacaoCriada.DataCriacao
        };
    }
}
