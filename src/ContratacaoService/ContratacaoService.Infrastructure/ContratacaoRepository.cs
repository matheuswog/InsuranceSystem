using Microsoft.EntityFrameworkCore;
using ContratacaoService.Application.Interfaces;
using ContratacaoService.Domain;

namespace ContratacaoService.Infrastructure;

public class ContratacaoRepository : IContratacaoRepository
{
    private readonly ContratacaoDbContext _context;

    public ContratacaoRepository(ContratacaoDbContext context)
    {
        _context = context;
    }

    public async Task<Contratacao?> ObterPorIdAsync(Guid id)
    {
        return await _context.Contratacoes.FindAsync(id);
    }

    public async Task<IEnumerable<Contratacao>> ObterTodasAsync()
    {
        return await _context.Contratacoes.ToListAsync();
    }

    public async Task<Contratacao?> ObterPorPropostaIdAsync(Guid propostaId)
    {
        return await _context.Contratacoes
            .FirstOrDefaultAsync(c => c.PropostaId == propostaId);
    }

    public async Task<Contratacao> CriarAsync(Contratacao contratacao)
    {
        _context.Contratacoes.Add(contratacao);
        await _context.SaveChangesAsync();
        return contratacao;
    }
}
