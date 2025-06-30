namespace Questao5.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public string Tipo { get; set; }

        public DomainException(string tipo, string mensagem) : base(mensagem)
        {
            Tipo = tipo;
        }
    }
}
