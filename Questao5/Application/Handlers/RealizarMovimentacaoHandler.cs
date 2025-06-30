using Dapper;
using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Exceptions;
using System.Data;

namespace Questao5.Application.Handlers
{
    public class RealizarMovimentacaoHandler : IRequestHandler<RealizarMovimentacaoCommand, string>
    {
        private readonly IDbConnection _db;

        public RealizarMovimentacaoHandler(IDbConnection db)
        {
            _db = db;
        }

        public async Task<string> Handle(RealizarMovimentacaoCommand request, CancellationToken cancellationToken)
        {
            var conta = await _db.QueryFirstOrDefaultAsync<Domain.Entities.ContaCorrente>(
                "SELECT * FROM contacorrente WHERE idcontacorrente = @Id",
                new { Id = request.IdContaCorrente });

            if (conta == null)
                throw new DomainException("INVALID_ACCOUNT", "Conta não encontrada.");

            if (!conta.Ativo)
                throw new DomainException("INACTIVE_ACCOUNT", "Conta inativa.");

            if (request.Valor <= 0)
                throw new DomainException("INVALID_VALUE", "Valor deve ser positivo.");

            if (request.TipoMovimento != "C" && request.TipoMovimento != "D")
                throw new DomainException("INVALID_TYPE", "Tipo de movimento inválido.");

            var id = Guid.NewGuid().ToString();

            await _db.ExecuteAsync(
                "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) " +
                "VALUES (@Id, @ContaCorrenteId, @Data, @Tipo, @Valor)",
                new
                {
                    Id = id,
                    ContaCorrenteId = request.IdContaCorrente,
                    Data = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                    Tipo = request.TipoMovimento,
                    Valor = request.Valor
                });

            return id;
        }
    }
}