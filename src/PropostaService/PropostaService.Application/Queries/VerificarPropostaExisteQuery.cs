using PropostaService.Application.Interfaces;

namespace PropostaService.Application.Queries;

public class VerificarPropostaExisteQuery
{
    public Guid Id { get; set; }
}

public class VerificarPropostaExisteQueryHandler
{
    private readonly IPropostaRepository _propostaRepository;

    public VerificarPropostaExisteQueryHandler(IPropostaRepository propostaRepository)
    {
        _propostaRepository = propostaRepository;
    }

    public async Task<bool> HandleAsync(VerificarPropostaExisteQuery query)
    {
        return await _propostaRepository.ExisteAsync(query.Id);
    }
}
