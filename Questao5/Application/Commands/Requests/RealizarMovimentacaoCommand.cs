using MediatR;

namespace Questao5.Application.Commands.Requests
{
    public class RealizarMovimentacaoCommand : IRequest<string>
    {
        public string IdMovimento { get; set; }
        public string IdContaCorrente { get; set; }
        public decimal Valor { get; set; }
        public string TipoMovimento { get; set; } // C ou D
    }
}
