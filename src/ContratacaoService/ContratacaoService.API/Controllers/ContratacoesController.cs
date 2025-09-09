using Microsoft.AspNetCore.Mvc;
using ContratacaoService.Application.Commands;
using ContratacaoService.Application.Queries;
using ContratacaoService.Application.DTOs;

namespace ContratacaoService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContratacoesController : ControllerBase
{
    private readonly CriarContratacaoCommandHandler _criarContratacaoHandler;
    private readonly ObterContratacaoQueryHandler _obterContratacaoHandler;
    private readonly ListarContratacoesQueryHandler _listarContratacoesHandler;

    public ContratacoesController(
        CriarContratacaoCommandHandler criarContratacaoHandler,
        ObterContratacaoQueryHandler obterContratacaoHandler,
        ListarContratacoesQueryHandler listarContratacoesHandler)
    {
        _criarContratacaoHandler = criarContratacaoHandler;
        _obterContratacaoHandler = obterContratacaoHandler;
        _listarContratacoesHandler = listarContratacoesHandler;
    }

    [HttpPost]
    public async Task<ActionResult<ContratacaoDto>> CriarContratacao([FromBody] CriarContratacaoDto dto)
    {
        var command = new CriarContratacaoCommand { Dto = dto };
        var resultado = await _criarContratacaoHandler.HandleAsync(command);
        
        if (resultado == null)
            return BadRequest("Proposta não encontrada, não está aprovada ou já foi contratada");
            
        return CreatedAtAction(nameof(ObterContratacao), new { id = resultado.Id }, resultado);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContratacaoDto>>> ListarContratacoes([FromQuery] Guid? propostaId)
    {
        var query = new ListarContratacoesQuery { PropostaId = propostaId };
        var resultado = await _listarContratacoesHandler.HandleAsync(query);
        return Ok(resultado);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContratacaoDto>> ObterContratacao(Guid id)
    {
        var query = new ObterContratacaoQuery { Id = id };
        var resultado = await _obterContratacaoHandler.HandleAsync(query);
        
        if (resultado == null)
            return NotFound();
            
        return Ok(resultado);
    }

    [HttpGet("por-proposta/{propostaId}")]
    public async Task<ActionResult<ContratacaoDto>> ObterContratacaoPorProposta(Guid propostaId)
    {
        var query = new ListarContratacoesQuery { PropostaId = propostaId };
        var resultado = await _listarContratacoesHandler.HandleAsync(query);
        
        var contratacao = resultado.FirstOrDefault();
        if (contratacao == null)
            return NotFound();
            
        return Ok(contratacao);
    }
}
