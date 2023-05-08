using MediatR;

namespace ExemploInversaoDependencia.Application.Commands
{
    public class IncluirReservaCmd : IRequest<IncluirReservaResponse>
    {
        public long CpfCliente { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }
    }
}
