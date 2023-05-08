using ExemploInversaoDependencia.Application.Connectors;
using ExemploInversaoDependencia.Domain;

namespace ExemploInversaoDependencia.InfraNova
{
    public class ClienteWcfClient : IClienteConsulta
    {
        public async Task<Cliente> ObterCliente(long cpfCliente)
        {
            return new Cliente
            {
                Cpf = 1111,
                Email = "reserva@central.com",
                Nome = "maria"
            };
        }
    }
}
