using ExemploInversaoDependencia.Application.Connectors;
using ExemploInversaoDependencia.Domain;
using MediatR;

namespace ExemploInversaoDependencia.Application.Commands
{
    internal class IncluirReservaCmdHandler : IRequestHandler<IncluirReservaCmd, IncluirReservaResponse>
    {
        private IClienteConsulta ClienteCli { get; }
        private IReservaRepository Repo { get; }

        public IncluirReservaCmdHandler(IClienteConsulta cli, 
                                        IReservaRepository repo)
        {
            ClienteCli = cli;
            Repo = repo;
        }

        public async Task<IncluirReservaResponse> Handle(IncluirReservaCmd request, CancellationToken cancellationToken)
        {
            var dadosCli = await ClienteCli.ObterCliente(request.CpfCliente);

            if (dadosCli is null)
            {
                return new IncluirReservaResponse(false, "");
            }
            else
            {
                var novaReserva = Reserva.CriarNova(request.DataEntrada,
                                                    request.DataSaida,
                                                    request.CpfCliente,
                                                    dadosCli.Nome);

                var sucesso = await Repo.Incluir(novaReserva);

                return new IncluirReservaResponse(sucesso, novaReserva.NumeroReserva);
            }
        }
    }
}
