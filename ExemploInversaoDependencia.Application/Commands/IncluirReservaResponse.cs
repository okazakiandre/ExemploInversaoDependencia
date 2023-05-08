namespace ExemploInversaoDependencia.Application.Commands
{
    public record IncluirReservaResponse
    {
        public IncluirReservaResponse() { }

        public IncluirReservaResponse(bool sucesso, string mensagem, string numero)
        {
            NumeroReserva = numero;
            Mensagem = mensagem;
            Sucesso = sucesso;
        }

        public IncluirReservaResponse(bool sucesso, string numero)
        {
            NumeroReserva = numero;
            Sucesso = sucesso;
            if (sucesso)
            {
                Mensagem = "Operação realizada com sucesso.";
            }
            else
            {
                Mensagem = "Ocorreu um erro, tente novamente mais tarde.";
            }
        }

        public string NumeroReserva { get; set; }
        public string Mensagem { get; set; }
        public bool Sucesso { get; set; }
    }
}
