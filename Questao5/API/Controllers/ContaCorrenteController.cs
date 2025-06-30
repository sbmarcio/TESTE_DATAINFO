using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Exceptions;

namespace Questao5.API.Controllers
{
    [ApiController]
    [Route("api/conta-corrente")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("movimentar")]
        public async Task<IActionResult> Movimentar([FromBody] RealizarMovimentacaoCommand command)
        {
            try
            {
                var id = await _mediator.Send(command);
                return Ok(new { Id = id });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message, tipo = ex.Tipo });
            }
        }

        [HttpGet("saldo/{contaId}")]
        public async Task<IActionResult> Saldo(string contaId)
        {
            try
            {
                var result = await _mediator.Send(new ConsultarSaldoQuery { ContaCorrenteId = contaId });
                return Ok(result);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { erro = ex.Message, tipo = ex.Tipo });
            }
        }
    }
}