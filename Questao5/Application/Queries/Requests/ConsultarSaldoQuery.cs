using MediatR;

namespace Questao5.Application.Queries.Requests
{
    public class ConsultarSaldoQuery : IRequest<object>
    {
        public string ContaCorrenteId { get; set; }
    }
}
