using ExemploInversaoDependencia.Domain;

namespace ExemploInversaoDependencia.Application.Connectors
{
    public interface IClienteConsulta
    {
        Task<Cliente> ObterCliente(long cpfCliente);
    }
}
