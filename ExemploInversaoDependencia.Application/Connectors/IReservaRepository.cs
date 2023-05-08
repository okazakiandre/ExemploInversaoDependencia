using ExemploInversaoDependencia.Domain;

namespace ExemploInversaoDependencia.Application.Connectors
{
    public interface IReservaRepository
    {
        Task<bool> Incluir(Reserva rsv);
    }
}
