using Dapper;
using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Exceptions;
using System.Data;

namespace Questao5.Application.Handlers
{
    public class ConsultarSaldoHandler : IRequestHandler<ConsultarSaldoQuery, object>
    {
        private readonly IDbConnection _db;

        public ConsultarSaldoHandler(IDbConnection db)
        {
            _db = db;
        }

        public async Task<object> Handle(ConsultarSaldoQuery request, CancellationToken cancellationToken)
        {
            var conta = await _db.QueryFirstOrDefaultAsync<Domain.Entities.ContaCorrente>(
                "SELECT * FROM contacorrente WHERE idcontacorrente = @Id",
                new { Id = request.ContaCorrenteId });

            if (conta == null)
                throw new DomainException("INVALID_ACCOUNT", "Conta não encontrada.");

            if (!conta.Ativo)
                throw new DomainException("INACTIVE_ACCOUNT", "Conta inativa.");

            var creditos = await _db.ExecuteScalarAsync<decimal>(
                "SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @Id AND tipomovimento = 'C'",
                new { Id = request.ContaCorrenteId });

            var debitos = await _db.ExecuteScalarAsync<decimal>(
                "SELECT COALESCE(SUM(valor), 0) FROM movimento WHERE idcontacorrente = @Id AND tipomovimento = 'D'",
                new { Id = request.ContaCorrenteId });

            return new
            {
                Numero = conta.Numero,
                Nome = conta.Nome,
                DataHora = DateTime.UtcNow,
                Saldo = creditos - debitos
            };
        }
    }
}