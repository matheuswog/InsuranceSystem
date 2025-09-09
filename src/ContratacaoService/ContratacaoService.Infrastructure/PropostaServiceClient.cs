using System.Text.Json;
using ContratacaoService.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContratacaoService.Infrastructure;

public class PropostaServiceClient : IPropostaServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<PropostaServiceClient> _logger;

    public PropostaServiceClient(HttpClient httpClient, ILogger<PropostaServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<bool> VerificarPropostaExisteAsync(Guid propostaId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/propostas/{propostaId}/existe");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao verificar se proposta {PropostaId} existe", propostaId);
            return false;
        }
    }

    public async Task<bool> VerificarPropostaAprovadaAsync(Guid propostaId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/propostas/{propostaId}");
            if (!response.IsSuccessStatusCode)
                return false;

            var content = await response.Content.ReadAsStringAsync();
            var proposta = JsonSerializer.Deserialize<PropostaResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return proposta?.Status == 2; // StatusProposta.Aprovada
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao verificar se proposta {PropostaId} está aprovada", propostaId);
            return false;
        }
    }

    private record PropostaResponse(int Status);
}
