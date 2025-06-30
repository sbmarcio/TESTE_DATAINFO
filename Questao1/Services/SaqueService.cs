using Questao1.Domain.Entities;
using Questao1.Domain.Interfaces;

namespace Questao1.Services
{
    public class SaqueService : ISaqueService
    {
        private readonly ContaBancaria _conta;

        public SaqueService(ContaBancaria conta)
        {
            _conta = conta;
        }

        public void Sacar(double valor)
        {
           _conta.Sacar(valor);
        }
    }
}


