using Microsoft.EntityFrameworkCore;
using PropostaService.Application.Interfaces;
using PropostaService.Domain;

namespace PropostaService.Infrastructure;

public class PropostaRepository : IPropostaRepository
{
    private readonly PropostaDbContext _context;

    public PropostaRepository(PropostaDbContext context)
    {
        _context = context;
    }

    public async Task<Proposta?> ObterPorIdAsync(Guid id)
    {
        return await _context.Propostas.FindAsync(id);
    }

    public async Task<IEnumerable<Proposta>> ObterTodasAsync()
    {
        return await _context.Propostas.ToListAsync();
    }

    public async Task<IEnumerable<Proposta>> ObterPorStatusAsync(StatusProposta status)
    {
        return await _context.Propostas
            .Where(p => p.Status == status)
            .ToListAsync();
    }

    public async Task<Proposta> CriarAsync(Proposta proposta)
    {
        _context.Propostas.Add(proposta);
        await _context.SaveChangesAsync();
        return proposta;
    }

    public async Task<Proposta> AtualizarAsync(Proposta proposta)
    {
        _context.Propostas.Update(proposta);
        await _context.SaveChangesAsync();
        return proposta;
    }

    public async Task<bool> ExisteAsync(Guid id)
    {
        return await _context.Propostas.AnyAsync(p => p.Id == id);
    }
}
