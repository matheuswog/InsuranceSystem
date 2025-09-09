using ContratacaoService.Domain;

namespace ContratacaoService.Tests;

public class ContratacaoTests
{
    [Fact]
    public void CriarContratacao_DeveCriarComDadosCorretos()
    {
        // Arrange
        var propostaId = Guid.NewGuid();
        var observacoes = "Contratação realizada";

        // Act
        var contratacao = new Contratacao(propostaId, observacoes);

        // Assert
        Assert.NotEqual(Guid.Empty, contratacao.Id);
        Assert.Equal(propostaId, contratacao.PropostaId);
        Assert.Equal(observacoes, contratacao.Observacoes);
        Assert.True(contratacao.DataContratacao <= DateTime.UtcNow);
        Assert.True(contratacao.DataCriacao <= DateTime.UtcNow);
    }

    [Fact]
    public void CriarContratacao_SemObservacoes_DeveCriarComObservacoesNull()
    {
        // Arrange
        var propostaId = Guid.NewGuid();

        // Act
        var contratacao = new Contratacao(propostaId);

        // Assert
        Assert.Equal(propostaId, contratacao.PropostaId);
        Assert.Null(contratacao.Observacoes);
    }
}
