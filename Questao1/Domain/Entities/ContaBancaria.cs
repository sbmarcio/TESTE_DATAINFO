using System.Globalization;
using System;

namespace Questao1.Domain.Entities
{
    public class ContaBancaria 
    {
        private const double TaxaFixaSaque = 3.50;
        public int Numero { get; }
        public string Titular { get; set; }
        public double Saldo { get; private set; }

        public ContaBancaria(int numero, string titular, double saldoInicial = 0.0)
        {
            Numero = numero;
            Titular = titular;
            Saldo = saldoInicial;
        }

        public void Depositar(double valor)
        {
            Saldo += valor;
        }

        public void Sacar(double valor)
        {
            Saldo -= valor + TaxaFixaSaque;
        }

        public override string ToString()
        {
            return $"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo:F2}";
        }
    }
}
