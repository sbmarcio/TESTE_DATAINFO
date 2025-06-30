using Questao1.Domain.Entities;
using Questao1.Domain.Interfaces;
using System;

namespace Questao1.Services
{
    public class DepositoService : IDepositoService
    {
        private readonly ContaBancaria _contaBancaria;
        private const double TaxaFixaSaque = 3.50;

        public DepositoService(ContaBancaria contaBancaria)
        {
            _contaBancaria = contaBancaria;
        }

        public void Depositar(double valor)
        {
            _contaBancaria.Depositar(valor);
        }
    }
}
