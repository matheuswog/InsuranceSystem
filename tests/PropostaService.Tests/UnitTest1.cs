using PropostaService.Domain;

namespace PropostaService.Tests;

public class PropostaDomainTests
{
    [Fact]
    public void CriarProposta_DeveCriarComStatusEmAnalise()
    {
        // Arrange
        var nomeCliente = "João Silva";
        var cpfCliente = "12345678901";
        var emailCliente = "joao@email.com";
        var valorSegurado = 100000m;
        var valorPremio = 1000m;

        // Act
        var proposta = new Proposta(nomeCliente, cpfCliente, emailCliente, valorSegurado, valorPremio);

        // Assert
        Assert.Equal(StatusProposta.EmAnalise, proposta.Status);
        Assert.Equal(nomeCliente, proposta.NomeCliente);
        Assert.Equal(cpfCliente, proposta.CpfCliente);
        Assert.Equal(emailCliente, proposta.EmailCliente);
        Assert.Equal(valorSegurado, proposta.ValorSegurado);
        Assert.Equal(valorPremio, proposta.ValorPremio);
        Assert.True(proposta.DataCriacao <= DateTime.UtcNow);
    }

    [Fact]
    public void AlterarStatus_DeveAlterarStatusEAtualizarData()
    {
        // Arrange
        var proposta = new Proposta("João Silva", "12345678901", "joao@email.com", 100000m, 1000m);
        var novoStatus = StatusProposta.Aprovada;
        var observacoes = "Aprovada para contratação";

        // Act
        proposta.AlterarStatus(novoStatus, observacoes);

        // Assert
        Assert.Equal(novoStatus, proposta.Status);
        Assert.Equal(observacoes, proposta.Observacoes);
        Assert.NotNull(proposta.DataAtualizacao);
    }

    [Fact]
    public void PodeSerContratada_DeveRetornarTrueApenasSeAprovada()
    {
        // Arrange
        var proposta = new Proposta("João Silva", "12345678901", "joao@email.com", 100000m, 1000m);

        // Act & Assert
        Assert.False(proposta.PodeSerContratada()); // Em Análise

        proposta.AlterarStatus(StatusProposta.Aprovada);
        Assert.True(proposta.PodeSerContratada()); // Aprovada

        proposta.AlterarStatus(StatusProposta.Rejeitada);
        Assert.False(proposta.PodeSerContratada()); // Rejeitada
    }
}
