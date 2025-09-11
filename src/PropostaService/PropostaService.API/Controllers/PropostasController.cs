using Microsoft.AspNetCore.Mvc;
using PropostaService.Application.Commands;
using PropostaService.Application.Queries;
using PropostaService.Application.DTOs;
using PropostaService.Domain;

namespace PropostaService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropostasController : ControllerBase
{
    private readonly CriarPropostaCommandHandler _criarPropostaHandler;
    private readonly AlterarStatusPropostaCommandHandler _alterarStatusHandler;
    private readonly ObterPropostaQueryHandler _obterPropostaHandler;
    private readonly ListarPropostasQueryHandler _listarPropostasHandler;
    private readonly VerificarPropostaExisteQueryHandler _verificarExisteHandler;

    public PropostasController(
        CriarPropostaCommandHandler criarPropostaHandler,
        AlterarStatusPropostaCommandHandler alterarStatusHandler,
        ObterPropostaQueryHandler obterPropostaHandler,
        ListarPropostasQueryHandler listarPropostasHandler,
        VerificarPropostaExisteQueryHandler verificarExisteHandler)
    {
        _criarPropostaHandler = criarPropostaHandler;
        _alterarStatusHandler = alterarStatusHandler;
        _obterPropostaHandler = obterPropostaHandler;
        _listarPropostasHandler = listarPropostasHandler;
        _verificarExisteHandler = verificarExisteHandler;
    }

    [HttpPost]
    public async Task<ActionResult<PropostaDto>> CriarProposta([FromBody] CriarPropostaDto dto)
    {
        // TODO: Adicionar validação de CPF
        var command = new CriarPropostaCommand { Dto = dto };
        var resultado = await _criarPropostaHandler.HandleAsync(command);
        return CreatedAtAction(nameof(ObterProposta), new { id = resultado.Id }, resultado);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropostaDto>>> ListarPropostas([FromQuery] StatusProposta? status)
    {
        var query = new ListarPropostasQuery { Status = status };
        var resultado = await _listarPropostasHandler.HandleAsync(query);
        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PropostaDto>> ObterProposta(Guid id)
    {
        var query = new ObterPropostaQuery { Id = id };
        var resultado = await _obterPropostaHandler.HandleAsync(query);
        
        if (resultado == null)
            return NotFound();
            
        return Ok(resultado);
    }

    [HttpGet("por-status/{status}")]
    public async Task<ActionResult<IEnumerable<PropostaDto>>> ListarPropostasPorStatus(StatusProposta status)
    {
        var query = new ListarPropostasQuery { Status = status };
        var resultado = await _listarPropostasHandler.HandleAsync(query);
        return Ok(resultado);
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult<PropostaDto>> AlterarStatusProposta(Guid id, [FromBody] AlterarStatusPropostaDto dto)
    {
        var command = new AlterarStatusPropostaCommand { Id = id, Dto = dto };
        var resultado = await _alterarStatusHandler.HandleAsync(command);
        
        if (resultado == null)
            return NotFound();
            
        return Ok(resultado);
    }

    [HttpGet("{id}/existe")]
    public async Task<ActionResult<bool>> VerificarPropostaExiste(Guid id)
    {
        var query = new VerificarPropostaExisteQuery { Id = id };
        var resultado = await _verificarExisteHandler.HandleAsync(query);
        return Ok(resultado);
    }
}
