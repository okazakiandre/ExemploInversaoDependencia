using MediatR;

namespace ExemploInversaoDependencia.Application.Commands
{
    public class IncluirReservaCmd : IRequest<IncluirReservaResponse>
    {
        public IncluirReservaCmd()
        {
        }

        public IncluirReservaCmd(long cpf, DateTime entrada, DateTime saida)
        {
            CpfCliente = cpf;
            DataEntrada = entrada;
            DataSaida = saida;
        }

        public long CpfCliente { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
    }
}
